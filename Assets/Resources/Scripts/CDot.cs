using UnityEngine;
using System.Collections;

public class CDot : MonoBehaviour {

	public GameObject owner;

	// Update is called once per frame
	void Update () {
		if (owner == null) {
			Destroy(this.gameObject);
		}
	}
}
