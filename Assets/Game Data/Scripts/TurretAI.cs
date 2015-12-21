using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]
public class TurretAI : Bolt.EntityBehaviour<IPositionState> {

	public int team;
	public float turnSpeed;
	public float fireSpeed = 0.5f;
	public Transform barrelOrigin;

	[SerializeField] private Animator _anim;
	[SerializeField] private GameObject target;
	[SerializeField] private List<GameObject> enemyTanksInRange = new List<GameObject>();
	private float lastFired = 0;

	/*void Start() {
		if (BoltNetwork.isServer && !entity.isAttached) {
			BoltNetwork.Instantiate (this.gameObject, transform.position, Quaternion.identity);
			Destroy (gameObject);
		} else if (!entity.isAttached) {
			Destroy(gameObject);
		}
	}

	public override void Attached() {
		state.Transform.SetTransforms (transform);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {

			if(other.GetComponent<LesterWeapon>() != null) {
				if(other.GetComponent<LesterWeapon>().state.Bool2) return;
			}

			int otherTeam = UserDataBlob.GetTeam(other.gameObject);

			if(team != otherTeam) {
				enemyTanksInRange.Add(other.gameObject);
			}

			EvaluateTargets();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {
			if(enemyTanksInRange.Contains(other.gameObject)) {
				enemyTanksInRange.Remove(other.gameObject);	
			}

			EvaluateTargets();
		}
	}

	void Update() {
		if (entity.isOwner) {
			if (target != null) {
				FaceTarget ();

				if (Time.time > lastFired + fireSpeed) {
					Fire ();
					lastFired = Time.time;
				}
			} else {
				transform.Rotate (0, 0, 50 * BoltNetwork.frameDeltaTime);
			}
		}
	}

	void FaceTarget() {
		var dir = target.transform.position - transform.position;
		var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		angle -= 90;
		float ang = Mathf.LerpAngle (transform.eulerAngles.z, angle, Time.deltaTime * turnSpeed);
		transform.rotation = Quaternion.AngleAxis (ang, Vector3.forward);
	}

	void Fire() {

		TurretShotEvent tse = TurretShotEvent.Create (Bolt.GlobalTargets.Everyone);
		tse.Origin = barrelOrigin.transform.position;
		tse.Direction = (barrelOrigin.position - transform.position);
		tse.TurretID = entity.networkId;
		tse.Send ();
	}

	void EvaluateTargets() {

		List<GameObject> toRemove = new List<GameObject>();

		foreach (GameObject go in enemyTanksInRange) {
			if(go == null) toRemove.Add(go);
		}

		foreach (GameObject go in toRemove) {
			enemyTanksInRange.Remove(go);
		}

		if (enemyTanksInRange.Count > 1) {
			target = enemyTanksInRange[0];
			float dist = Vector2.Distance(transform.position, target.transform.position);

			foreach(GameObject t in enemyTanksInRange) {
				if( Vector2.Distance(transform.position, t.transform.position) < dist) {
					target = t;
				}
			}
		} else if (enemyTanksInRange.Count > 0) {
			target = enemyTanksInRange [0];
		} else {
			target = null;
		}
	}*/
}
