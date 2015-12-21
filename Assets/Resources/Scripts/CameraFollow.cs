using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public float zPos;
	public Transform target;
	public GameObject perspectiveCamera;
	
	void LateUpdate() {
		if (target != null) {
			transform.position = new Vector3 (target.position.x, target.position.y, zPos);
		}
	}
}
