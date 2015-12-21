using UnityEngine;
using System.Collections;
using System;

public class KennethWeapon : ExtendableAbility {

    AbilityObject rocket;
    AbilityObject bomb;

    void Start()
    {
        rocket = state.Abilities[0];
        bomb = state.Abilities[1];

        if(entity.isOwner)
        {
            bomb.Integer1 = KennethData.data.carryingCapacity;
            bomb.Float1 = KennethData.data.bombGenerationTime;
        }
    }
	
	void Update () {
        if(entity.hasControl)
        {
            if(InputHandler.GetMouseDown(1) && rocket.Cooldown == 0)
            {
                SendAbility(0);
            }

            if(Input.GetKeyDown(KeyCode.Space) && bomb.Integer1 > 0)
            {
                SendAbility(1);
            }
        }

        if(entity.isOwner)
        {
            if (bomb.Integer1 < KennethData.data.carryingCapacity && bomb.Float1 == 0)
            {
                bomb.Integer1++;
                bomb.Float1 = KennethData.data.bombGenerationTime;
            } else if (bomb.Integer1 < KennethData.data.carryingCapacity && bomb.Float1 > 0)
            {
                bomb.Float1 = Mathf.Clamp(bomb.Float1 - Time.deltaTime, 0, Mathf.Infinity);
            }
        }
	}

    public override void OnAbility(int code)
    {
        if(code == 0 && rocket.Cooldown == 0)
        {
            rocket.Cooldown = KennethData.data.rocketCooldown;

            BoltNetwork.Instantiate(SkinAssets.GetGameObject("Rocket"), transform.position, Quaternion.identity)
                .GetComponent<BoltEntity>()
                .GetState<IOwnedEntity>().Owner = entity;

        } else if (code == 1 && bomb.Integer1 > 0)
        {
            BoltNetwork.Instantiate(SkinAssets.GetGameObject("Bomb"), transform.position, transform.rotation)
                .GetComponent<BoltEntity>()
                .GetState<IOwnedEntity>().Owner = entity;

            bomb.Integer1--;
        }
    }

    public override void OnEntityAbility(int code)
    {
        //nothing to do
    }
}
