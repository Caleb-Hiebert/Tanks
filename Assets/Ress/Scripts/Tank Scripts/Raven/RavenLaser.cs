using UnityEngine;
using System.Collections;

public abstract class RavenLaser : Bolt.EntityBehaviour<IPlayer> {

    bool laserCache;
    public Transform barrelPoint, barrel;

    AbilityObject laser;

    public Vector2 laserOrigin;
    public Vector2 laserEnd;

    float dmgTick = 0;

    void Start()
    {
        laser = state.Abilities[0];
    }

    void Update()
    {
        if (laserCache != laser.Boolean1)
        {
            laserCache = laser.Boolean1;
            ToggleLaser();
        }

        if(laserCache)
        {
            dmgTick += Time.deltaTime;

            laserOrigin = barrel.position;

            var hit = Physics2D.Raycast(barrelPoint.position, barrelPoint.transform.TransformPoint(Vector3.up) - barrelPoint.position, RavenData.data.laserDistance, ClientCallbacks.mask);

            if(hit.collider != null)
            {
                laserEnd = hit.point;

                if(hit.collider.tag == "Player" && BoltNetwork.isServer)
                {
                    if(hit.collider.GetComponentInParent<BoltEntity>().Team() != entity.Team())
                    {
                        if(dmgTick > 1 / RavenData.data.laserTicksPerSecond)
                        {
                            dmgTick = 0;
                            hit.collider.GetComponentInParent<BoltEntity>().Damage(entity, RavenData.data.laserDamagePerTick);
                        }
                    }
                }

            } else {
                laserEnd = barrelPoint.transform.TransformPoint(0, RavenData.data.laserDistance, 0);
            }
        }
    }

    public void ToggleLaser()
    {
        if(laserCache)
        {
            Debug.LogWarning("RAVEN LASER ON");
            LaserOn();
        } else
        {
            Debug.LogWarning("RAVEN LASER OFF");
            LaserOff();
        }
    }

    public abstract void LaserOn();
    public abstract void LaserOff();

    public bool LaserEnabled
    {
        get
        {
            return laserCache;
        }
    }
}
