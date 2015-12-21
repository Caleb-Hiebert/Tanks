using UnityEngine;
using System.Collections;

public class ReducedDamageShield : Bolt.EntityBehaviour<IPlayer> {

    public SpriteRenderer shield;
    public Gradient dmgFlash;
    public float flashLength;

    float time;

    public override void Attached()
    { 
    state.AddCallback("HP", OnHPChange);
        state.AddCallback("Powerups.ArmorModifier", OnArmorChange);
        OnArmorChange();
	}
	
	void Update () {
        if (entity.isAttached && state.Powerups.ArmorModifier != 1)
        {
            time = Mathf.Clamp(time + Time.deltaTime, 0, flashLength);
            shield.color = dmgFlash.Evaluate(time / flashLength);
        }
	}

    void OnHPChange()
    {
        time = 0;
    }

    void OnArmorChange()
    {
        if(state.Powerups.ArmorModifier != 1)
        {
            if (!shield.enabled)
                shield.enabled = true;
        } else if (state.Powerups.ArmorModifier == 1)
        {
            shield.enabled = false;
        }
    }
}
