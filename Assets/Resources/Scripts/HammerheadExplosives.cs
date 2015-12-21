using UnityEngine;
using System.Collections;
using System;

public class HammerheadExplosives : ExtendableAbility {

	public GameObject hammerHead;

    public AbilityObject explosiveAbility;

    void Start()
    {
        explosiveAbility = state.Abilities[1];

        if (entity.isOwner)
        {
            explosiveAbility.Boolean1 = false;
            explosiveAbility.Integer1 = HammerheadData.data.explosiveCarryingCapacity;
            explosiveAbility.Float1 = HammerheadData.data.explosivePrimingTime;
        }
    }

    void Update()
    {
        if(entity.hasControl)
        {
            if(explosiveAbility.Integer1 > 0 && !explosiveAbility.Boolean1 && InputHandler.GetMouseDown(1))
            {
                SendAbility(1);
            }
        } 

        if(entity.isOwner)
        {
            if(explosiveAbility.Float1 == 0 && explosiveAbility.Integer1 < HammerheadData.data.explosiveCarryingCapacity)
            {
                explosiveAbility.Integer1++;
                explosiveAbility.Float1 = HammerheadData.data.explosivePrimingTime;

            } else if(explosiveAbility.Float1 > 0 && explosiveAbility.Integer1 < HammerheadData.data.explosiveCarryingCapacity)
            {
                explosiveAbility.Float1 = Mathf.Clamp(explosiveAbility.Float1 - Time.deltaTime, 0, Mathf.Infinity);
            }
        }

        hammerHead.SetActive(explosiveAbility.Boolean1);
    }

    public override void OnAbility(int code)
    {

        var ability = state.Abilities[1];

        if (code == 1 && !ability.Boolean1)
        {
            state.Abilities[1].Integer1 -= 1;
            ability.Boolean1 = true;
        }
    }

    public void Detonate(Collider2D other)
    {
        state.Abilities[1].Boolean1 = false;

        var boom = BoltNetwork.Instantiate(SkinAssets.GetGameObject("ConeExplosion"), hammerHead.transform.position, hammerHead.transform.rotation);

        if(other.tag == "Player")
        {
            if(other.GetComponentInParent<BoltEntity>().Team() != entity.Team())
            {
                other.GetComponentInParent<BoltEntity>().Damage(entity, HammerheadData.data.explosiveDamage);
            }
        }
    }

    public override void OnEntityAbility(int code)
    {
    }
}
