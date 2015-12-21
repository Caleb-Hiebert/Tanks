using UnityEngine;
using System.Collections;
using System;

public class GaryUI : ExtendableUI {

    Ability mainCanon;
    Ability sniperToggle;
    Ability cdReset;

    public override void InitilizeAbilities(ExtendableAbility ability)
    {
        if(ability == null)
        {
            Debug.LogWarning("Gary's Ability script was null!!");
            return;
        }

        if(ability.GetType() == typeof(GaryWeapon))
        {
            var weap = (GaryWeapon)ability;

            sniperToggle = CreateAbility("Sniper Aimer", "Pew Pew", 0);
            cdReset = CreateAbility("Instant Reload", "reloads big cannon", GaryData.data.instantReloadCooldown);
            mainCanon = CreateAbility("Main Cannon", "Shoots de big one", GaryData.data.mainCooldown);

            sniperToggle.Text = "";
            sniperToggle.Seconds = 0;
            mainCanon.Text = "";
            cdReset.Text = "";
        }

        AddonBarEnabled = false;
    }

    void Update()
    {
        if (!initilized)
            return;

        mainCanon.Seconds = state.Abilities[0].Cooldown;
        sniperToggle.CooldownPercentage = state.Abilities[1].Boolean1 ? 1 : 0;
        cdReset.Seconds = state.Abilities[2].Cooldown;

        if(state.Abilities[0].Cooldown == 0)
        {
            mainCanon.CooldownPercentage = 1;
            mainCanon.Color = Color.green;
        } else
        {
            mainCanon.ResetColor();
        }

        if (state.Abilities[2].Cooldown == 0)
        {
            cdReset.CooldownPercentage = 1;
            cdReset.Color = Color.green;
        }
        else
        {
            cdReset.ResetColor();
        }
    }
}
