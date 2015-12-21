using UnityEngine;
using System.Collections;
using System;

public class HammerheadEarthquake : ExtendableAbility {

	private float lastUsed = -50;

	void Update () {
        if(Input.GetKey(KeyCode.Space) && state.Abilities[2].Cooldown == 0)
        {
            SendAbility(2);
        }
	}
	
	void DropTheBass() {
        if (!BoltNetwork.isServer)
            return;

        BoltNetwork.Instantiate(SkinAssets.GetGameObject("Earthquake"), transform.position, Quaternion.identity).GetComponent<BoltEntity>().GetState<IOwnedEntity>().Owner = entity;
	}

    public override void OnAbility(int code)
    {
        var ability = state.Abilities[2];

        if(code == 2 && ability.Cooldown == 0)
        {
            DropTheBass();
            ability.Cooldown = HammerheadData.data.earthquakeCooldown;
        }
    }

    public override void OnEntityAbility(int code)
    {
        //nothing to do
    }
}
