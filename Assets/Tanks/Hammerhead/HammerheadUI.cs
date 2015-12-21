using UnityEngine;
using System.Collections;
using System;

public class HammerheadUI : ExtendableUI {

    private Ability grapple;
    private Ability explosives;
    private Ability earthQuake;

    public override void InitilizeAbilities(ExtendableAbility a)
    {
        if (a == null)
        {
            Debug.LogWarning("One of Hammerhead's abilities was null!!");
            return;
        }

        if(a.GetType() == typeof(HammerheadGrapple))
        {
            var g = (HammerheadGrapple)a;

            grapple = CreateAbility("Grapple", "Grapple", HammerheadData.data.grappleCooldown);
            grapple.Text = "";
            grapple.Seconds = 0;
        } else if (a.GetType() == typeof(HammerheadExplosives))
        {
            explosives = CreateAbility("Explosives", "Explosives", HammerheadData.data.explosivePrimingTime);
            explosives.Text = state.Abilities[1].Integer1.ToString();
            explosives.Seconds = 0;
        } else if (a.GetType() == typeof(HammerheadEarthquake))
        {
            var earthquake = (HammerheadEarthquake)a;

            earthQuake = CreateAbility("Earthquake", "Earthquake", HammerheadData.data.earthquakeCooldown);
            earthQuake.Text = "";
            earthQuake.Seconds = 0;
        }

        AddonBarEnabled = false;
    }

    void Update()
    {
        if (!initilized)
            return;

        if (entity.hasControl)
        {
            var explosiveState = state.Abilities[1];

            if (state.Abilities[0].Cooldown > 0)
            {
                grapple.ResetColor();
                grapple.Seconds = state.Abilities[0].Cooldown;
            }
            else
            {
                grapple.Seconds = 0;
                grapple.CooldownPercentage = 1;
                grapple.Color = Color.green;
            }

            if (state.Abilities[2].Cooldown > 0)
            {
                earthQuake.ResetColor();
                earthQuake.Seconds = state.Abilities[2].Cooldown;
            }
            else
            {
                earthQuake.Seconds = 0;
                earthQuake.CooldownPercentage = 1;
                earthQuake.Color = Color.green;
            }

            explosives.CooldownPercentage = 1 - (explosiveState.Float1 / HammerheadData.data.explosivePrimingTime);
            explosives.Text = explosiveState.Integer1.ToString();
        }
    }
}
