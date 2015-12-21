using UnityEngine;
using System.Collections;
using System;

public class KennethUI : ExtendableUI {

    Ability rocket;
    Ability bomb;

    AbilityObject r, b;

    void Update () {
        if (!initilized)
            return;

        if(r.Cooldown == 0)
        {
            rocket.CooldownPercentage = 1;
            rocket.Color = Color.green;
            rocket.Seconds = 0;
        } else
        {
            rocket.ResetColor();
            rocket.Seconds = r.Cooldown;
        }

        bomb.CooldownPercentage = b.Float1 / KennethData.data.bombGenerationTime;
        bomb.Text = b.Integer1.ToString();
	}

    public override void InitilizeAbilities(ExtendableAbility ability)
    {
        if(ability == null)
        {
            Debug.LogWarning("Kenneth's Weapon was null!!!");
            return;
        }

        if(ability.GetType() == typeof(KennethWeapon))
        {
            r = state.Abilities[0];
            b = state.Abilities[1];

            rocket = CreateAbility("Rocket", "RocketDesc", KennethData.data.rocketCooldown);
            rocket.Text = "";
            rocket.Seconds = 0;

            bomb = CreateAbility("Bomb", "BombDesc", KennethData.data.bombGenerationTime);
            bomb.Seconds = 0;
            bomb.Text = b.Integer1.ToString();

            AddonBarEnabled = false;
        }
    }
}
