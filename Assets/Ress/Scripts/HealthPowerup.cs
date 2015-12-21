using UnityEngine;
using System.Collections;

public class HealthPowerup : Powerup {

	public CircleCollider2D cCollider;
	public SpriteRenderer image;
	public ParticleSystem pSystem;
	public int healthAmount;

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
    }

    public override void OnCollided(BoltEntity tank)
    {
        tank.GetComponent<TankHealth>().Heal(entity, healthAmount);
        Disable();
    }
}
