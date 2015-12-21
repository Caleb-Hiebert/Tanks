using UnityEngine;
using System.Collections;

public class Turbulence : MonoBehaviour {

	public float duration = 7;
	public float shakeLength = 0.02f;
	public float intensity = 2000;

	private Vector3 shake;
	private float startTime;
	private float shakeTime = -20;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (Time.time > startTime + duration) {
			Destroy(this);
		}
		
		if (Time.time > shakeTime + shakeLength) {
			shake = new Vector3 (Random.Range (-intensity, intensity), Random.Range (-intensity, intensity));
			shakeTime = Time.time;
		}
		
		transform.position = Vector3.Lerp (transform.position, (transform.position + shake), shakeLength * Time.deltaTime);
	}
}
