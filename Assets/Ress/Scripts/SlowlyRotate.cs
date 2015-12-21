using UnityEngine;
using System.Collections;

public class SlowlyRotate : MonoBehaviour {

	public float degreesPerSecond;
	public bool serverRotate;

	// Update is called once per frame
	void Update () {
		if (serverRotate) {
			if (!BoltNetwork.isServer)
				return;
			transform.Rotate (0, 0, degreesPerSecond * Time.deltaTime);
		} else {
			transform.Rotate (0, 0, degreesPerSecond * Time.deltaTime);
		}
	}
}
