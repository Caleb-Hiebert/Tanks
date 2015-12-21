using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(TerrainSampler))]
public class Motor : Bolt.EntityBehaviour<IPlayer> {

    public float currentSpeed, rpmFromVelocity = 0.08f;
    Rigidbody2D body;

    private float stunnedUntil = 0, engineRPM, realRPM;
    private List<MoveMod> movementModifications;
    private TerrainSampler terrainSampler;

    void Start()
    {
        movementModifications = new List<MoveMod>();
        body = GetComponent<Rigidbody2D>();
        terrainSampler = GetComponent<TerrainSampler>();
    }

    void FixedUpdate()
    {
        if (state.Stunned)
            return;

        if (entity.hasControl)
        {
            currentSpeed = 0;

            if (InputHandler.GetAxis("Horizontal") != 0)
            {
                transform.Rotate(0, 0, Input.GetAxis("Horizontal") * state.Movement.Turnspeed * Time.deltaTime);
                body.angularVelocity = Input.GetAxis("Horizontal") * state.Movement.Turnspeed * Time.deltaTime;
            }

            if (InputHandler.GetAxis("Vertical") != 0)
            {
                currentSpeed += state.Movement.Speed;
            }

            if (InputHandler.GetAxis("Boost") != 0)
            {
                currentSpeed += state.Movement.BoostSpeed;
            }

            var terrain = terrainSampler.Get();

            if (terrain.angularDrag != 0 && terrain.drag != 0)
            {
                RigidBody.angularDrag = terrain.angularDrag;
                RigidBody.drag = terrain.drag;
            }
            else
            {
                RigidBody.angularDrag = 7;
                RigidBody.drag = 5;
            }
        }

        if(entity.isOwner)
        {
            int speed = 0;

            for (int i = 0; i < movementModifications.Count; i++)
            {
                var m = movementModifications[i];

                if (Time.time < m.addTime + m.deathDelay)
                {
                    speed += (int)m.modAmount;
                }
                else
                {
                    movementModifications.Remove(m);
                }
            }

            if(state.Powerups.SpeedPowerup != speed)
            {
                state.Powerups.SpeedPowerup = speed;
            }
        }

        if (entity.hasControl)
        {

            currentSpeed += state.Powerups.SpeedPowerup;

            currentSpeed = Mathf.Round(currentSpeed);

            body.AddRelativeForce(Vector2.up * (currentSpeed * BoltNetwork.frameDeltaTime * 50) * InputHandler.GetAxis("Vertical"));

            float accelVolume = (Utils.Positivize(Input.GetAxis("Vertical")) + Input.GetAxis("Boost") + Utils.Positivize(InputHandler.GetAxis("Horizontal")));
            float movementVolume = body.velocity.magnitude * rpmFromVelocity;

            realRPM = movementVolume + accelVolume;

            if (engineRPM < realRPM)
                engineRPM += 8f * BoltNetwork.frameDeltaTime;
            else if (engineRPM > realRPM)
                engineRPM -= 8f * BoltNetwork.frameDeltaTime;
        }
    }

    public void AddMoveMod(float modAmount, float time)
    {
        movementModifications.Add(new MoveMod(modAmount, time));
    }

    public float RPM
    {
        get { return Mathf.Round(engineRPM * 100) / 100; }
    }

    public void Stun(float length)
    {
        if (Time.time > stunnedUntil)
        {
            stunnedUntil = Time.time + length;
        }
        else
        {
            stunnedUntil += length;
        }
    }

    public Rigidbody2D RigidBody
    {
        get { return body; }
    }
}

class MoveMod
{
    public float modAmount;
    public float deathDelay;
    public float addTime;

    public MoveMod(float modAmount, float delay)
    {
        this.modAmount = modAmount;
        this.deathDelay = delay;
        this.addTime = Time.time;
    }
}
