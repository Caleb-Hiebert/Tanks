using UnityEngine;
using System.Collections;

public class TimothyWeapon : Bolt.EntityEventListener<IPlayer> {

	public Transform barrelOrigin;
	public SpriteRenderer shield;
	public PolygonCollider2D shieldCollider;
	public LineRenderer laser;
	public LayerMask laserMask;
	public float energy = 100;
	public Transform laser2Origin;
	private float lastHealTime = -50;
	private float floatDmg;
	private float floatHealing;
	private Sound sound;

	/*public override void Attached () {
		state.AddCallback ("WeaponON", WeaponCallback);
		sound = GetComponent<Sound> ();
	}


	void Update() {

		if (entity.isOwner) {
			if (InputHandler.ability1 && energy > 0 && !InputHandler.ability2) {
				energy -= 10 * Time.deltaTime;
				state.WeaponON = true;
			} else {
				state.WeaponON = false;
			}
			
			if (InputHandler.ability2 && energy > 0) {
				state.Bool2 = true;
				energy -= 15 * Time.deltaTime;
			} else {
				state.Bool2 = false;
			}
			
			if (InputHandler.ability1 && InputHandler.ability2 && energy > 50) {
				
				Vector2 mousePos = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
				Vector2 barrelFirePoint = new Vector2 (laser2Origin.position.x, laser2Origin.position.y);
				
				energy -= 50;
				WardenLaser2Event wle = WardenLaser2Event.Create(Bolt.GlobalTargets.Everyone);
				//wle.Sender = GameMaster.gm.LocalUserData;
				wle.Origin = barrelFirePoint;
				wle.Direction = mousePos - barrelFirePoint;
				wle.Send();
			}
			
			if (energy < 100 && !InputHandler.ability1 && !InputHandler.ability2) {
				energy += 8 * Time.deltaTime;
			}
			
			if (energy >= 100) {
				energy = 100;
			}
			
			if (InputHandler.ability3 && energy > 30 && Time.time > lastHealTime + 30) {
				//GetComponent<TankHealth>().ModifyHealth("Healing", 250);
				energy -= 30;
				lastHealTime = Time.time;
			}
			
			TankUI.ui.SliderAmount = energy / 100;
			TankUI.ui.UpperText = Mathf.RoundToInt(energy) + "/100";
			TankUI.ui.upperText.alignment = TextAnchor.MiddleCenter;
			
			if (Time.time < lastHealTime + 30) {
				TankUI.ui.SecondaryCooldown = (Time.time - lastHealTime) / 30;
				TankUI.ui.SecondaryCooldownText = Mathf.RoundToInt(30 - (Time.time - lastHealTime)) + "s";
			} else {
				TankUI.ui.SecondaryCooldown = 1;
				TankUI.ui.SecondaryCooldownText = "";
			}
		}


		if (state.WeaponON) {
			laser.enabled = true;
			Vector2 mousePos = new Vector2(state.CursorPosition.x, state.CursorPosition.y);
			Vector2 barrelFirePoint = new Vector2 (barrelOrigin.position.x, barrelOrigin.position.y);
			
			RaycastHit2D hit = Physics2D.Raycast (barrelFirePoint, (mousePos - barrelFirePoint), 500, laserMask);
			
			if (hit.collider != null) {
				laser.SetPosition(0, new Vector3(barrelOrigin.position.x, barrelOrigin.position.y, 0));
				laser.SetPosition(1, new Vector3(hit.point.x, hit.point.y, 0));

				Destroy(Instantiate(Resources.Load("ShootingEffects/LaserImpact"), hit.point, Quaternion.identity), 0.2f);

				try {
					//hit.collider.GetComponent<Damageable>().Damage(50);
				} catch {
				}

				if(hit.collider.gameObject == GameMaster.gm.userData.activeTank && !entity.isOwner) {

					//UserDataBlob other = GetComponent<TankNetworkController>().userData;

					/*if(other.state.Team == GameMaster.gm.userData.state.Team) {
						floatHealing += 140 * Time.deltaTime;

						if(floatHealing >= 1) {
							//GameMaster.gm.userData.activeTank.GetComponent<TankHealth>().ModifyHealth("Time", 1);
							floatHealing = 0;
						}
					} else {
						floatDmg += 30 * Time.deltaTime;

						if(floatDmg >= 1) {
							//GameMaster.gm.userData.activeTank.GetComponent<TankHealth>().ModifyHealth(other.entity.networkId, -1);
							floatDmg = 0;
						}
					}
				} else {
				}

				try { 
					if(BoltNetwork.isServer) {
						//hit.collider.gameObject.GetComponent<Damageable>().Damage(1
					}
				} catch {}
				
			} else {

				Vector3 endpoint = transform.position - Camera.main.ScreenToWorldPoint (Input.mousePosition) * 10;
				endpoint.z = 0;
				Debug.DrawLine(transform.position, endpoint, Color.red, 0.5f);

				laser.SetPosition(0, new Vector3(barrelOrigin.position.x, barrelOrigin.position.y, 0));
				laser.SetPosition(1, barrelFirePoint + ((mousePos - barrelFirePoint).normalized * 500));
			}

		} else {
			laser.enabled = false;
		}

		if (state.Bool2) {
			shield.enabled = true;
			shieldCollider.enabled = true;
		} else {
			shield.enabled = false;
			shieldCollider.enabled = false;
		}
	}

	void WeaponCallback() {
		if (state.WeaponON) {
			sound.PlaySound("Beam");
		} else {
			sound.StopSound("Beam");
		}
	}*/
}
