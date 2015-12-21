using UnityEngine;
using System.Collections;

public class ShieldPowerup : Powerup {

    public SpriteRenderer sr;
    public CircleCollider2D circleCollider;

    public float duration;
    public float amount;

    public override void OnEnabled()
    {
        sr.enabled = true;
        circleCollider.enabled = true;
    }

    public override void OnDisabled()
    {
        sr.enabled = false;
        circleCollider.enabled = false;
    }

    public override void OnCollided(BoltEntity tank)
    {
        tank.GetComponentInParent<TankHealth>().SetArmor(amount, duration);

        Disable();
    }
}
