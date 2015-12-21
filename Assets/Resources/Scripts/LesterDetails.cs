using UnityEngine;
using System.Collections;

public class LesterDetails : Bolt.EntityBehaviour<IPlayer> {
	
	public float dummyFactor;
	public bool reverse;

	void Update () {

		if (!entity.isAttached)
			return;

		if (reverse) {
			transform.Rotate (0, 0, state.Movement.RPM * 360 * Time.deltaTime * dummyFactor * -1);
		} else {
			transform.Rotate (0, 0, state.Movement.RPM * 360 * Time.deltaTime * dummyFactor);
		}
	}
}
