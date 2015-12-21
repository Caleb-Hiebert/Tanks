using UnityEngine;
using System.Collections;

public class HealOverTime : Bolt.EntityBehaviour<IPlayer> {

	public float healingDelay;
	public float ticksPerSecond;
    public int healthPerTick;
    float lastDamageTime;
    int lastHealth;

	public override void Attached() {
        if (entity.isOwner)
        {
            state.AddCallback("HP", OnHealthChange);
            InvokeRepeating("Heal", healingDelay, 1 / ticksPerSecond);
        }
	}
	
	void Heal() {
		if (Time.time > lastDamageTime + healingDelay && state.HP < state.MaxHealth) {
            GetComponentInChildren<TankHealth>().Heal(entity, healthPerTick);
        }
	}

    void OnHealthChange()
    {
        if(state.HP < lastHealth)
        {
            lastDamageTime = Time.time;
        }

        lastHealth = state.HP;
    }
}
