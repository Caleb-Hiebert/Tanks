using UnityEngine;
using System.Collections;

public class Stun : MonoBehaviour {

	public delegate void StunEvent(GameObject stunnedTank, float duration);
	public static event StunEvent OnStun;

	public delegate void StunFinishedEvent(GameObject stunnedTank);
	public static event StunFinishedEvent OnStunFinished;

	public float duration = 2;
	
	void Awake() {
		//GetComponent<TankMotor> ().Stun (duration);
		//GetComponent<TankNetworkController> ().state.Stunned = true;
		Invoke ("Rekt", duration);

		if (OnStun != null) {
			Debug.LogWarning("STUN BROADCAST");
			OnStun(gameObject, duration);
		}
	}

	void Rekt() {
		//GetComponent<TankNetworkController> ().state.Stunned = false;

		if (OnStunFinished != null) {
			OnStunFinished(gameObject);
			Debug.LogWarning("STUN FINISHED");
		}

		Destroy (this);
	}
}
