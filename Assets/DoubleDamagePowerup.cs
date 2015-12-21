using UnityEngine;
using System.Collections;
using System;

public class DoubleDamagePowerup : Powerup {

    public SpriteRenderer spr;
    public CircleCollider2D col;
    public float dmgModifier;
    public float duration;

    public override void OnCollided(BoltEntity tank)
    {
        tank.GetState<IPlayer>().Powerups.DamageModifier = dmgModifier;
        StartCoroutine(DisableAfterTime(tank, duration));
        Disable();
    }

    public override void OnDisabled()
    {
        spr.enabled = false;
        col.enabled = false;
    }

    public override void OnEnabled()
    {
        spr.enabled = true;
        col.enabled = true;
    }

    IEnumerator DisableAfterTime(BoltEntity be, float time)
    {
        yield return new WaitForSeconds(time);

        if(be != null)
        {
            be.GetState<IPlayer>().Powerups.DamageModifier = 1;
        }
    }
}
