using UnityEngine;
using System.Collections;
using System;

public class RavenWeapon : ExtendableAbility {

    AbilityObject laser;
    AbilityObject invis;
    AbilityObject flash;

    public override void OnAbility(int code)
    {
        if (code == 0)
        {
            laser.Boolean1 = !laser.Boolean1;
            state.Invisible = false;
        } else if (code == 1)
        {
            if(invis.Cooldown == 0 && !laser.Boolean1)
            {
                state.Invisible = true;
                StartCoroutine(BecomeVisible(RavenData.data.invisibilityDuration));
                invis.Cooldown = RavenData.data.invisibilityCooldown;
            }
        } else if (code == 2)
        {
            if(flash.Cooldown == 0)
            {
                flash.Cooldown = RavenData.data.flashCooldown;
            }
        }
    }

    public override void OnEntityAbility(int code)
    {
        //Nothing to do
    }

    void Start()
    {
        laser = state.Abilities[0];
        invis = state.Abilities[1];
        flash = state.Abilities[2];

        if (entity.isOwner)
        {
            laser.Boolean1 = false;
        }
    }

    void Update()
    {
        if (!entity.hasControl)
            return;
        
        if(InputHandler.GetMouseDown(0) || Input.GetMouseButtonUp(0))
        {
            SendAbility(0);
        }

        if(InputHandler.GetMouseDown(1))
        {
            SendAbility(1);
        }

        if(InputHandler.GetKeyDown(KeyCode.Space) && flash.Cooldown == 0)
        {
            SendAbility(2);

            GetComponentInParent<Rigidbody2D>().MovePosition(transform.TransformPoint(Vector3.up * RavenData.data.flashDistance));
        }
    }

    IEnumerator BecomeVisible(float delay)
    {
        yield return new WaitForSeconds(delay);

        state.Invisible = false;
    }
}
