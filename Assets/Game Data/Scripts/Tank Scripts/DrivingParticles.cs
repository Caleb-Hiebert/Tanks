using UnityEngine;
using System.Collections;

public class DrivingParticles : MonoBehaviour {

    TerrainSampler terrain;
    [SerializeField]
    ParticleSystem drivingSystem;

	void Start () {
        terrain = GetComponentInParent<TerrainSampler>();
	}
	
	void Update () {
        drivingSystem.startColor = terrain.Get().color;
	}
}
