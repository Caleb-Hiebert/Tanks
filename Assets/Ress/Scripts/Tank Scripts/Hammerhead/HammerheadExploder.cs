using UnityEngine;
using System.Collections;

public class HammerheadExploder : MonoBehaviour {

	public LayerMask things;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == things && BoltNetwork.isServer) {
			GetComponentInParent<HammerheadExplosives>().Detonate(other);
		}
	}
}
