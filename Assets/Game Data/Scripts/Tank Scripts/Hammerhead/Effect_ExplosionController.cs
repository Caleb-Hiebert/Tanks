using UnityEngine;
using System.Collections;

public class Effect_ExplosionController : MonoBehaviour {

	public float size;
	public float shockwaveTravelTime;
	public float shockwaveLife;
	private CircleCollider2D explosionRadius;
	private float lerpTime;

	// Use this for initialization
	void Start () {
		explosionRadius = gameObject.AddComponent<CircleCollider2D>();
		Destroy (this.explosionRadius, shockwaveLife);
		Destroy (this, shockwaveLife);
	}
	
	// Update is called once per frame
	void Update () {
		if(lerpTime < shockwaveTravelTime)lerpTime += Time.deltaTime;
		explosionRadius.radius = (size * Mathf.Sin(lerpTime / shockwaveTravelTime * Mathf.PI * 0.8f));
	}
}
