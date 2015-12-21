using UnityEngine;
using System.Collections;

public class SpeedPowerup : Powerup {

	public CircleCollider2D cCollider;
	public SpriteRenderer image;
	public int speedAmount = 40;
    public float duration = 5;
	public ParticleSystem pSystem;
    public ParticleSystem onPickedUp;

    public override void OnCollided(BoltEntity tank)
    {
        tank.GetComponentInChildren<Motor>().AddMoveMod(speedAmount, duration);
        Disable();
    }

    public override void OnEnabled()
    {
        cCollider.enabled = true;
        image.enabled = true;
        pSystem.enableEmission = true;
    }

    public override void OnDisabled()
    {
        cCollider.enabled = false;
        image.enabled = false;
        pSystem.enableEmission = false;
        onPickedUp.Play();
    }
}
