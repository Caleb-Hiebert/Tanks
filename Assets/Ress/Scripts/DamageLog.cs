using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageLog {

	public int damage;
	public string damager;

	public DamageLog (string damager, int damage) {
		this.damage = damage;
		this.damager = damager;
	}

	public static void AddDamage(List<DamageLog> array, string damageDealer, int damageDealt) {
		foreach (DamageLog d in array) {
			if(d.damager == damageDealer) {
				d.damage +=  damageDealt;
				return;
			}
		}

		DamageLog dmg = new DamageLog(damageDealer, damageDealt);

		array.Add (dmg);
	}
}
