using UnityEngine;
using System.Collections;

public class DetachableParticleSystem : MonoBehaviour {

	Transform parent;
	
	void Awake() {
		parent = transform.parent;
		transform.SetParent (null);
	}

	void Update () {
		if (parent != null) {
			transform.position = parent.position;
		} else {
			Destroy(gameObject, GetComponent<ParticleSystem>().duration);
		}
	}
}
