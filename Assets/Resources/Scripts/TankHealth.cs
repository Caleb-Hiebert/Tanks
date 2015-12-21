using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TankHealth : Bolt.EntityBehaviour<IPlayer> {

	private List<HealthChange> damageLog;
    private List<Shield> shields = new List<Shield>();

    public override void Attached()
    {
        if (entity.isOwner)
        {
            damageLog = new List<HealthChange>();
        }
    }

    public void Damage(BoltEntity player, int amount)
    {
        if (state.State.State <= 1)
            return;

        amount = amount < 0 ? amount * -1 : amount;

        amount = Mathf.RoundToInt(amount * state.Powerups.ArmorModifier);

        if (player.StateIs<IPlayer>())
        {
            amount = (int)(amount * player.GetState<IPlayer>().Powerups.DamageModifier);

            player.GetState<IPlayer>().Stats.DamageDealt += amount;
        }

        damageLog.Add(new HealthChange(player, -amount));

        TakeFromShield(amount);

        amount = amount - state.Shield < 0 ? 0 : amount - state.Shield;

        UpdateShield();

        state.Stats.DamageTaken += amount;

        state.HP -= amount;

        if(state.HP <= 0)
        {
            GetComponent<PlayerController>().Kill();
            damageLog.Clear();
        }
    }

    int TakeFromShield(int amount)
    {
        if (shields.Count == 0)
            return amount;

        var shield = shields[0];

        if(shield.amount > amount)
        {
            shield.amount -= amount;

            state.Stats.DamageShielded += amount;
        } else
        {
            int amt = shield.amount;

            state.Stats.DamageShielded += amt;

            shields.Remove(shield);

            TakeFromShield(amt);
        }

        return 0;
    }

    public void Heal(BoltEntity source, int amount)
    {
        if (state.State.State <= 1)
            return;

        damageLog.Add(new HealthChange(source, amount));

        if(state.HP < state.MaxHealth)
        {
            var tt = Mathf.Clamp(amount, 0, state.MaxHealth - state.HP);

            state.Stats.AmountHealed += tt;

            state.HP += tt;
        }
    }

    public void AddShield(float duration, int amount)
    {
        var newShield = new Shield(amount, duration);
        shields.Add(newShield);
        UpdateShield();

        state.Stats.TotalShield += amount;

        StartCoroutine(RemoveShield(newShield, duration));
    }

    void UpdateShield()
    {
        int shield = 0;

        foreach (var item in shields)
        {
            shield += item.amount;
        }

        state.Shield = shield;
    }

    public BoltEntity LastToDamage
    {
        get
        {
            for (int i = damageLog.Count - 1; i > 0; i--)
            {
                if (damageLog[i].amount < 0)
                {
                    return damageLog[i].source;
                }
            }

            return null;
        }
    }

    public BoltEntity[] Assistors
    {
        get
        {
            var klr = LastToDamage;

            float assistTime = 10;

            List<BoltEntity> assistors = new List<BoltEntity>();

            for (int i = damageLog.Count - 1; i > 0; i--)
            {
                var dl = damageLog[i];

                if (dl.source.StateIs<IPlayer>() && dl.source.Team() != entity.Team() && dl.amount < 0 && dl.time + assistTime > Time.time && dl.source != LastToDamage)
                {
                    assistors.Add(dl.source);
                }

                if (dl.time + assistTime < Time.time)
                    break;
            }

            return assistors.ToArray();
        }
    }

    public void ResetHP(int maxHealth)
    {
        if (!entity.isOwner)
            return;

        state.MaxHealth = state.HP = maxHealth;
    }

    //damage will be multiplied with armor so 25% reduction would be .75f
    public void SetArmor(float amout, float duration)
    {
        Invoke("RemoveArmor", duration);
        state.Powerups.ArmorModifier = amout;
    }

    IEnumerator RemoveShield(Shield shield, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (shield != null && shields.Contains(shield))
        {
            shields.Remove(shield);

            UpdateShield();
        }
    }

    void RemoveArmor()
    {
        state.Powerups.ArmorModifier = 1;
    }
}

public class HealthChange {
	public int amount;
	public BoltEntity source;
	public float time;

	public HealthChange (BoltEntity source, int amount) {
		this.source = source;
		this.amount = amount;
		this.time = Time.time;
	}
}

public class Shield
{
    public float duration;
    public int amount;
    private float birthTime;

    public Shield(int amount, float duration)
    {
        this.duration = duration;
        this.amount = amount;
        this.birthTime = Time.time;
    }

    public bool IsDead
    {
        get { return Time.time > birthTime + duration; }
    }
}
