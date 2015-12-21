using UnityEngine;
using System.Collections;

public class TankInfoDisplay : Bolt.EntityBehaviour<IPlayer> {

	public float effectsBarSpeed;
	public Color localBarColor;
	public Color blueTeamColor;
	public Color redTeamColor;

	public GameObject healthBar;
	public GameObject shieldBar;
	public GameObject effectsBar;
	public TextMesh nameDisplay;

    public GameObject infoPanel;

	[SerializeField] private float smoothHealth = 0;

    static int localTeam = -1;

    private int lastHealth;

    public override void Attached()
    {
        state.AddCallback("Team", OnTeamChanged);
        state.AddCallback("Shield", OnShieldChanged);
        state.AddCallback("Name", OnNameChanged);
        state.AddCallback("HP", OnHealthChanged);
        state.AddCallback("State.State", OnState);
        state.AddCallback("Invisible", OnInvisible);

        OnNameChanged();
        OnShieldChanged();
        OnTeamChanged();
        OnState();
        lastHealth = state.HP;
    }

    public override void ControlGained()
    {
        localTeam = state.Team;
        SetColors();
    }

    public override void ControlLost()
    {
        localTeam = -1;
        SetColors();
    }

    void OnTeamChanged()
    {
        if (entity.hasControl)
        {
            localTeam = state.Team;
        }

        SetColors();
    }

    public void Hide()
    {
        infoPanel.SetActive(false);
    }

    public void Show()
    {
        infoPanel.SetActive(true);
    }

	void Update() {
        float hp = state.HP;
        float maxHp = state.MaxHealth;

        if (hp == 0 || maxHp == 0)
            return;

        float hpAmount = hp / maxHp;

        smoothHealth = Mathf.Lerp(smoothHealth, hpAmount, effectsBarSpeed * Time.deltaTime);

        if (smoothHealth > hpAmount)
        {
            effectsBar.transform.localScale = new Vector3(smoothHealth, 1, 1);
            healthBar.transform.localScale = new Vector3(hpAmount, 1, 1);
        }
        else if (smoothHealth < hpAmount)
        {
            effectsBar.transform.localScale = new Vector3(hpAmount, 1, 1);
            healthBar.transform.localScale = new Vector3(smoothHealth, 1, 1);
        }
	}

	void SetColors() {
        var renderer = healthBar.GetComponent<SpriteRenderer>();

        if (entity.hasControl)
        {
            renderer.color = localBarColor;
        } else if (state.Team != localTeam)
        {
            renderer.color = redTeamColor;
        } else if (state.Team == localTeam)
        {
            renderer.color = blueTeamColor;
        } else
        {
            renderer.color = Color.magenta;
        }
	}

	void OnShieldChanged () {
        if (state.MaxHealth == 0)
        {
            shieldBar.transform.localScale = new Vector3(0, 0.5f, 0);
            return;
        }

        float shield = state.Shield;
        float maxHP = state.MaxHealth;

		shieldBar.transform.localScale = new Vector3 (shield / maxHP, 0.5f, 1);
	}

    void OnState()
    {
        if(state.State.State <= 1)
        {
            Hide();
        } else
        {
            Show();
        }
    }

    void OnNameChanged()
    {
        nameDisplay.text = state.Name;
    }

    void OnHealthChanged()
    {
        int change = state.HP - lastHealth;

        if (change > 20 || change < -20)
        {
            DamageIndicator.SpawnDamageIndicator(transform.position, change);
        }

        lastHealth = state.HP;
    }

    void OnInvisible()
    {
        if (state.Invisible)
        {
            if (entity.IsFriendly())
            {

            }
            else
            {
                Hide();
            }
        }
        else
        {
            Show();
        }
    }
}
