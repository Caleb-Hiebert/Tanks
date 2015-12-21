using UnityEngine;
using System.Collections;
using System;

public class RavenUI : ExtendableUI {

    Ability laser, invisibility, flash;
    AbilityObject l, f;

    public override void InitilizeAbilities(ExtendableAbility ability)
    {
        if(ability == null)
        {
            Debug.LogWarning("Raven's abilty was null!");
        }

        if(ability.GetType() == typeof(RavenWeapon))
        {
            laser = CreateAbility("Laser", "Laser", 0);
            laser.Text = string.Empty;
            laser.Seconds = 0;

            invisibility = CreateAbility("Invisibility", "Invis", RavenData.data.invisibilityCooldown);
            invisibility.Text = string.Empty;
            invisibility.Seconds = 0;

            flash = CreateAbility("Flash", "Flash", RavenData.data.flashCooldown);
            flash.Text = "";
            flash.Seconds = 0;
        }
    }

    void Update()
    {
        if (!initilized)
            return;

        if (entity.isAttached && entity.hasControl)
        {
            invisibility.Seconds = state.Abilities[1].Cooldown;
            flash.Seconds = f.Cooldown;
        }
    }

    void Start()
    {
        l = state.Abilities[0];
        f = state.Abilities[2];
    }
}
