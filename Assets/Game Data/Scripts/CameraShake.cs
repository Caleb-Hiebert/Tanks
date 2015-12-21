using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	public float shakeLength = 0.02f;
	public float intensity = 50;
	public float distanceFactor = 0.5f;
	public float maxDistance = 50;
	public float duration;
    public bool useCurve;
	public Vector3 shakeOrigin;

	private float startTime;
	private float shakeTime = -20.0f;
	private Vector3 shake;
	private float distMod = 1;
    private AnimationCurve falloffCurve;

	void Start () {

		if (UserSettings.cameraShake == 0) {
			DestroyImmediate(this);
		}

		startTime = Time.time;

		distMod = (maxDistance - Vector2.Distance (shakeOrigin, Camera.main.transform.position)) * distanceFactor;
		
		if (distMod <= 0) {
			Destroy (this);
		}
	}

	void LateUpdate () {

		if (Time.time > startTime + duration) {
			Destroy(this);
		}

        float curve = 1;

        if(useCurve)
        {
            curve = falloffCurve.Evaluate(Time.time - startTime);
        }

		if (Time.time > shakeTime + shakeLength) {
			shake = new Vector3 (Random.Range (-intensity, intensity) * distMod * curve * UserSettings.cameraShake, Random.Range (-intensity, intensity) * distMod * curve * UserSettings.cameraShake);
			shakeTime = Time.time;
		}

		transform.position = Vector3.Lerp (transform.position, (transform.position + shake), shakeLength * Time.deltaTime);
	}

	public static void Apply(Vector2 position, float intensity, float duration) {
		CameraShake cs = Camera.main.gameObject.AddComponent<CameraShake> ();
		
		cs.duration = duration;
		cs.intensity = intensity;
		cs.shakeOrigin = position;
	}

    public static void Apply(Vector2 position, float intensity, float duration, AnimationCurve falloffCurve)
    {
        CameraShake cs = Camera.main.gameObject.AddComponent<CameraShake>();

        cs.duration = duration;
        cs.intensity = intensity;
        cs.shakeOrigin = position;
        cs.falloffCurve = falloffCurve;
        cs.useCurve = true;
    }
}
