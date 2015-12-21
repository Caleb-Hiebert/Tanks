using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ability : MonoBehaviour {

    [SerializeField]
    private Text cooldownText, text;

    [SerializeField]
    private Image reloadImage;

    public string abilityName, abilityDescription;

    public float cooldown;

    private Color defaultColor;

    void Awake()
    {
        defaultColor = reloadImage.color;
    }

    public float Seconds
    {
        set
        {
            if (value == 0)
            {
                cooldownText.text = "";
                CooldownPercentage = 0;
            }
            else
            {
                cooldownText.text = string.Format("{0}s", Mathf.Ceil(value));
                CooldownPercentage = value / cooldown;
            }
        }
    }

    public string Text
    {
        set
        {
            text.text = value;
        }
    }

    public Color Color
    {
        set
        {
            reloadImage.color = value;
        }
    }

    public void ResetColor()
    {
        if(reloadImage.color != defaultColor)
        reloadImage.color = defaultColor;
    }

    public float CooldownPercentage
    {
        set { reloadImage.fillAmount = value; }
    }
}
