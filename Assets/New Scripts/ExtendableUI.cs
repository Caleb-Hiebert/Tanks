using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public abstract class ExtendableUI : Bolt.EntityBehaviour<IPlayer> {

    private Image ui_bar, ui_barEffects, ui_addonBar;
    private Text ui_hpText, ui_addonText;
    private GameObject ui_addonBarWhole, ui_abilityPanel;
    private List<Ability> abilities = new List<Ability>();

    private float smoothHP = 0;

    public bool initilized = false;

    public string HpText { get { return ui_hpText.text; } set { ui_hpText.text = value; } }
    public string AddonText { get { return ui_addonText.text; } set { ui_addonText.text = value; } }
    public float HpBar { get { return ui_bar.fillAmount; } set { ui_bar.fillAmount = value; } }
    public float HpBarEffects { get { return ui_barEffects.fillAmount; } set { ui_barEffects.fillAmount = value; } }
    public float AddonBar { get { return ui_addonBar.fillAmount; } set { ui_addonBar.fillAmount = value; } }
    public bool AddonBarEnabled { get { return ui_addonBarWhole.activeSelf; } set { ui_addonBarWhole.SetActive(value); } }

    void Awake()
    {
        GameObject.FindGameObjectWithTag("Interface").GetComponentInChildren<StaticUI>().Awake();

        ui_bar = StaticUI.singleton.bar;
        ui_barEffects = StaticUI.singleton.barEffects;
        ui_addonBar = StaticUI.singleton.addonBar;
        ui_hpText = StaticUI.singleton.hp;
        ui_addonText = StaticUI.singleton.addonText;
        ui_addonBarWhole = StaticUI.singleton.addon;
        ui_abilityPanel = StaticUI.singleton.abilityPanel;

        ClearAbilities();
        StartCoroutine(WeaponListener());
    }

    public abstract void InitilizeAbilities(ExtendableAbility ability);

    void LateUpdate()
    {
        if (entity.hasControl)
        {
            float hp = state.HP;
            float maxHP = state.MaxHealth;

            float hpFloat = hp / maxHP;

            smoothHP = Mathf.Lerp(smoothHP, hpFloat, 8 * Time.deltaTime);

            if (smoothHP < hpFloat)
            {
                HpBar = smoothHP;
                HpBarEffects = hpFloat;
            }
            else if (smoothHP > hpFloat)
            {
                HpBar = hpFloat;
                HpBarEffects = smoothHP;
            }

            HpText = string.Format("{0}/{1}", state.HP, state.MaxHealth);
        }
    }

    public Ability CreateAbility(string name, string description, float cooldown)
    {
        Debug.Log("Creating Ability! " + name);

        var newAbility = Instantiate(StaticUI.singleton.abilityPrefab);
        newAbility.transform.SetParent(ui_abilityPanel.transform, false);
        newAbility.GetComponent<Ability>().abilityName = name;
        newAbility.GetComponent<Ability>().abilityDescription = description;
        newAbility.GetComponent<Ability>().cooldown = cooldown;
        abilities.Add(newAbility.GetComponent<Ability>());

        ui_abilityPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(50 * abilities.Count, 48);

        return newAbility.GetComponent<Ability>();
    }

    public void RemoveAbility(Ability ability)
    {
        Destroy(ability.gameObject);
        abilities.Remove(ability);
    }

    public void ClearAbilities()
    {
        foreach (var item in abilities)
        {
            if(item != null)
                Destroy(item.gameObject);
        }

        abilities.Clear();
    }

    void OnDestroy()
    {
        ClearAbilities();
    }

    public T GetAbility<T>() where T : ExtendableAbility
    {
        return GetComponentInChildren<T>();
    }

    IEnumerator WeaponListener()
    {
        while(GetComponentInChildren<ExtendableAbility>() == null)
        {
            yield return null;
        }

        yield return null;

        if (!entity.hasControl)
        {
            Destroy(this);

            yield break;
        }

        foreach (var item in GetComponentsInChildren<ExtendableAbility>())
        {
            InitilizeAbilities(item);
        }

        initilized = true;

        yield return null;
    }
}
