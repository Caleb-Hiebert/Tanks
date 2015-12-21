using UnityEngine;
using System.Collections;
using System;

public class GaryWeapon : ExtendableAbility {
	public Transform barrelOrigin;
	private Sound sound;
	private bool rSound;

    void Update()
    {
        if (entity.isAttached)
        {
            var mainCannon = state.Abilities[0];
            var instantReload = state.Abilities[2];

            if (entity.hasControl)
            {
                if (InputHandler.GetMouseDown(0) && mainCannon.Cooldown == 0)
                {
                    SendAbility(0);
                } else if (Input.GetKeyDown(KeyCode.Space) && instantReload.Cooldown == 0)
                {
                    SendAbility(2);
                }
            }
        }
    }

    public override void OnAbility(int code)
    {
        if(code == 0 && state.Abilities[0].Cooldown == 0)
        {
            var origin = barrelOrigin.position;
            var direction = (state.MousePosition - transform.position).normalized;
            var angle = barrelOrigin.parent.eulerAngles.z;

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, 500, ClientCallbacks.mask);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Player" && hit.collider.GetComponentInParent<BoltEntity>().Team() != entity.Team())
                {
                    hit.collider.GetComponentInParent<TankHealth>().Damage(entity, state.Abilities[1].Boolean1 ? GaryData.data.sniperDamage : GaryData.data.mainDamage);
                }
            }

            var gs = GaryShot.Create(Bolt.GlobalTargets.Everyone);
            gs.Origin = origin;
            gs.Direction = direction;
            gs.Entity = entity;
            gs.Send();

            state.Abilities[0].Cooldown = GaryData.data.mainCooldown;

        } else if (code == 1)
        {
            state.Abilities[1].Boolean1 = !state.Abilities[1].Boolean1;
        } else if(code == 2 && state.Abilities[2].Cooldown == 0)
        {
            state.Abilities[0].Cooldown = 0;
            state.Abilities[2].Cooldown = GaryData.data.instantReloadCooldown;
        }
    }

    public override void OnEntityAbility(int code)
    {
        
    }
}
