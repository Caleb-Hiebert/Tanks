using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public float cameraShakeIntensity = 15;
	public float cameraShakeDuration = 0.5f;
    public bool useCurve = false;
    public AnimationCurve curve;

	void Start () {
        if (useCurve)
        {
            CameraShake.Apply(transform.position, cameraShakeIntensity, cameraShakeDuration, curve);
        }
        else
        {
            CameraShake.Apply(transform.position, cameraShakeIntensity, cameraShakeDuration);
        }

		if (BoltNetwork.isServer) {
			BoltNetwork.Instantiate(BoltPrefabs.BurnMarks, transform.position, Quaternion.identity);
		}
	}
}
