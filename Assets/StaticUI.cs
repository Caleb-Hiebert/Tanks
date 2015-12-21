using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StaticUI : MonoBehaviour {
    public static StaticUI singleton;

    public Image bar, barEffects, addonBar;
    public Text hp, addonText;
    public GameObject addon, abilityPanel;
    public GameObject abilityPrefab;

    public void Awake()
    {
        singleton = this;
    }
}
