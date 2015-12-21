using UnityEngine;
using System.Collections;

public class CameraScroller : MonoBehaviour {

	public float maxSize;
	public float maxPerspectiveSize = 13.5f;
	public GameObject perspectiveCam;

	private float minSize = 12;
	private Camera cam;
	
	void Start () {
		cam = GetComponent<Camera> ();

		if (perspectiveCam == null) {
			perspectiveCam = cam.transform.FindChild("PerspectiveCamera").gameObject;
		}
	}

	void Update () {
		cam.orthographicSize += Input.GetAxis ("ScrollWheel") * Time.deltaTime * 0.2f * -1.0f;

		if (cam.orthographicSize > maxSize)
			cam.orthographicSize = maxSize;
		else if (cam.orthographicSize < minSize)
			cam.orthographicSize = minSize;

		if (perspectiveCam != null) {

			float perspectiveZ = Utils.Remap (cam.orthographicSize, minSize, maxSize, 0, maxPerspectiveSize);
			perspectiveZ *= -1;
			perspectiveCam.transform.localPosition = new Vector3 (0, 0, -10 + perspectiveZ);
		} else {
			Debug.LogWarning("Could not find perspective camera");
		}
	}
}
