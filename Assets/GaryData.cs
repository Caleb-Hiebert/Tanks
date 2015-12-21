using UnityEngine;
using System.Collections;

public class GaryData : DataBase {

    public static GaryData data;

    public GameObject frostGary;
    public int mainDamage, sniperDamage;
    public float mainCooldown, instantReloadCooldown;


	void Awake () {
        data = this;
	}
}
