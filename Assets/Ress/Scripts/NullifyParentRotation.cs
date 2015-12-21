using UnityEngine;
using System.Collections;

public class NullifyParentRotation : MonoBehaviour {

	private Transform parent;

	// Use this for initialization
	void Start () {
		parent = GetComponentInParent<Transform> ();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float rot = parent.rotation.z * -1;
		transform.rotation = Quaternion.Euler (0, 0, rot);
	}
}
