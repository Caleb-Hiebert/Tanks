using UnityEngine;
using System.Collections;

public class KodiakWeapon : Bolt.EntityBehaviour<IPlayer> {

	/*public LayerMask fireMask;
	public ParticleSystem worldFire;
	public ParticleSystem localFire;
	public PolygonCollider2D fireCollider;
	public Transform barrelOrigin;
	public float damageInterval;
	public int damagePerInterval;
	public float moveSpeedBoost = 100;
	public float shieldLength;
	private float lastDamageTime = 0;
	private float lastSecondaryTime = -500;
	private float moveCounter = 0;
	private Sound sound;

	void Start() {
		TankUI.ui.LowerText = "---";
		TankUI.ui.UpperText = "---";
		sound = GetComponent<Sound> ();

		sound.StopSound("KodiakFire");
		worldFire.emissionRate = 0;
		localFire.emissionRate = 0;
		fireCollider.enabled = false;
	}

	public override void Attached () {
		state.AddCallback ("WeaponON", WeaponCallback);
	}

	void Update() {
		if (entity.isOwner) {
			if (InputHandler.ability3 && Time.time > lastSecondaryTime + 30) {
				//GetComponent<TankMotor>().AddMoveMod(moveSpeedBoost, shieldLength);
				//GetComponent<TankHealth>().AddShieldAmount(250);
				lastSecondaryTime = Time.time + shieldLength;
				moveCounter = shieldLength;
				Invoke("RemoveShield", shieldLength);
			}

			state.WeaponON = InputHandler.ability1;
			
			if(moveCounter > 0) {
				moveCounter -= BoltNetwork.frameDeltaTime;
				TankUI.ui.SecondaryCooldown = moveCounter / shieldLength;
				
			} else if (Time.time < lastSecondaryTime + 30 && moveCounter <= 0) {
				TankUI.ui.SecondaryCooldown = (Time.time - lastSecondaryTime) / 30;
				TankUI.ui.SecondaryCooldownText = Mathf.RoundToInt(30 - (Time.time - lastSecondaryTime)) + "s";
			} else {
				TankUI.ui.SecondaryCooldown = 1;
				TankUI.ui.SecondaryCooldownText = "";
			}
		}
	}

	void RemoveShield() {
		//GetComponent<TankHealth> ().RemoveShield ();
	}

	public void FireColliderTriggered(Collider2D other) {

		try {
			//other.GetComponent<Damageable>().Damage(1);
		} catch {
		}

		try {
			//other.GetComponentInParent<Damageable>().Damage(1);
		} catch {
		}

		/*if (other.tag == "Player" && entity.isOwner) {
			RaycastHit2D hit = Physics2D.Raycast(barrelOrigin.position, other.transform.position - barrelOrigin.position, 10.0f, fireMask);

			if(hit.collider != null && hit.collider.tag == "Player" && entity.isOwner && UserDataBlob.GetTeam(hit.collider.gameObject) != GameMaster.gm.userData.state.Team) {

				if(Time.time > lastDamageTime + damageInterval && entity.isOwner) {

					/*DamageEvent kfe = DamageEvent.Create(Bolt.GlobalTargets.Everyone);
					//kfe.Source = GameMaster.gm.LocalUserData;
					kfe.Damage = damagePerInterval;
					kfe.Destination = hit.collider.transform.root.GetComponent<TankNetworkController>().userData.entity.networkId;
					kfe.Send();

					lastDamageTime = Time.time;
				}
			}
		}
	}

	void WeaponCallback() {
		if (state.WeaponON) {
			sound.PlaySound("KodiakFire");
			worldFire.emissionRate = 350;
			localFire.emissionRate = 350;
			fireCollider.enabled = true;
		} else {
			sound.StopSound("KodiakFire");
			worldFire.emissionRate = 0;
			localFire.emissionRate = 0;
			fireCollider.enabled = false;
		}
	}*/
}
