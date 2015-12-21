using UnityEngine;
using System.Collections;

public class TankTracks : MonoBehaviour {

	private ParticleSystem pSystem;
	private float emissionRate;
    TerrainSampler terrain;

	public Transform parentTransform;

	void Start () {
		pSystem = GetComponent<ParticleSystem> ();
		parentTransform = GetComponentInParent<Transform> ();
		emissionRate = pSystem.emissionRate;
        pSystem.emissionRate = 0;
        terrain = parentTransform.GetComponentInParent<TerrainSampler>();

        Invoke("On", 0.5f);
	}
	
	void Update () {
		pSystem.startRotation = parentTransform.rotation.eulerAngles.z * Mathf.Deg2Rad * -1;

        if(terrain != null)
		if (terrain.Get().type == TerrainType.TerrainTypes.Water) {
			pSystem.emissionRate = 0;
		} else {
			pSystem.emissionRate = emissionRate;
		}
	}

    void On()
    {
        pSystem.emissionRate = emissionRate;
    }
}
