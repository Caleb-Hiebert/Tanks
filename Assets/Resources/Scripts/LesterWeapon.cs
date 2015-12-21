using UnityEngine;
using System.Collections;
using System;

public class LesterWeapon : ExtendableAbility {

	public Transform barrelOrigin;
	public LineRenderer laser;
	public LayerMask laserMask;
	public Transform flashPoint;
	public float laserRange = 5;
	public ParticleSystem laserAwesomeness;
	
	private float floatDmg;
	private float invisTime;
	private float lastInvisTime = -50;
	private float lastFlashTime = -30;
		
	/*public override void Attached () {
		state.AddCallback ("Bool2", Invis);
	}

	void Start() {
		if (entity.isOwner) {
			TankUI.ui.UpperText = "---";
		}
	}

	void Update() {
		if (entity.isOwner) {

			state.WeaponON = InputHandler.ability1;
			if(state.WeaponON) invisTime = 0;

			if(InputHandler.ability3 && Time.time > lastInvisTime + 40 && !state.Bool2) {
				invisTime = 10;
				state.Bool2 = true;
			}

			if(InputHandler.ability2 && Time.time > lastFlashTime + 30) {
				Destroy(Instantiate(Resources.Load("LocalPrefabs/Flash"), transform.position, Quaternion.identity), 1);
				lastFlashTime = Time.time;
				Vector3 fpp = new Vector3(flashPoint.position.x, flashPoint.position.y, 0);
				transform.position = fpp;
			}

			if(invisTime > 0) {
				invisTime -= Time.deltaTime;
				TankUI.ui.SecondaryCooldown = invisTime / 10;
				
			} else if (Time.time < lastInvisTime + 40 && invisTime <= 0) {
				TankUI.ui.SecondaryCooldown = (Time.time - lastInvisTime) / 40;
				TankUI.ui.SecondaryCooldownText = Mathf.RoundToInt(40 - (Time.time - lastInvisTime)) + "s";
			} else {
				TankUI.ui.SecondaryCooldown = 1;
				TankUI.ui.SecondaryCooldownText = "";
			}

			if(Time.time < lastFlashTime + 30) {
				TankUI.ui.SliderAmount = (Time.time - lastFlashTime) / 30;
				TankUI.ui.LowerText = Mathf.RoundToInt(30 - (Time.time - lastFlashTime)) + "s";
			} else {
				TankUI.ui.SliderAmount = 1;
				TankUI.ui.LowerText = "---";
			}

			if(invisTime <= 0) {
				state.Bool2 = false;
			}
		}

		if (state.WeaponON) {
			laser.enabled = true;
			laserAwesomeness.emissionRate = 250;
			Vector2 mousePos = new Vector2(state.CursorPosition.x, state.CursorPosition.y);
			Vector2 barrelFirePoint = new Vector2 (barrelOrigin.position.x, barrelOrigin.position.y);
			
			RaycastHit2D hit = Physics2D.Raycast (barrelFirePoint, (mousePos - barrelFirePoint), laserRange, laserMask);
			
			if (hit.collider != null) {
				laser.SetPosition(0, new Vector3(barrelOrigin.parent.position.x, barrelOrigin.parent.position.y, -1));
				laser.SetPosition(1, new Vector3(hit.point.x, hit.point.y, -1));
			
				Destroy(Instantiate(Resources.Load("ShootingEffects/LaserImpactRed"), hit.point, Quaternion.identity), 0.2f);
				laser.SetWidth(0.6f, 1.2f);
				
				try {
					//hit.collider.GetComponent<Damageable>().Damage(1);
				} catch {
				}

				try {
					//hit.collider.GetComponentInParent<Damageable>().Damage(1);
				} catch {
				}

				/*if(hit.collider.gameObject == GameMaster.gm.userData.activeTank) {
					
					//UserDataBlob other = GetComponent<TankNetworkController>().userData;
					
					/*if(other.state.Team != GameMaster.gm.userData.state.Team) {
						floatDmg += 150 * Time.deltaTime;

						if(floatDmg >= 5) {
							//GameMaster.gm.userData.activeTank.GetComponent<TankHealth>().ModifyHealth(other.entity.networkId, -5);
							floatDmg -= 5;
						}
					}
				}
			} else {
				laser.SetPosition(0, new Vector3(barrelOrigin.parent.position.x, barrelOrigin.parent.position.y, 0));
				laser.SetPosition(1, barrelFirePoint + ((mousePos - barrelFirePoint).normalized * laserRange));
				laser.SetWidth(0.7f, 0.2f);
			}
			
		} else {
			laser.enabled = false;
			laserAwesomeness.emissionRate = 0;
		}
	}

	void Invis() {

		if (state.Bool2) {

			GetComponent<Invis>().InvisEnabled = true;

			if (entity.isOwner) {
				lastInvisTime = Time.time + 10;
			}
		} else {
			GetComponent<Invis>().InvisEnabled = false;
		}
	}*/

    public override void OnAbility(int code)
    {
        throw new NotImplementedException();
    }

    public override void OnEntityAbility(int code)
    {
        throw new NotImplementedException();
    }
}
