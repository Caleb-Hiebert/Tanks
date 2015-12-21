using UnityEngine;
using System.Collections;

public class FadeSpriteOverLife : MonoBehaviour {
		
	float lifeTime;
	SpriteRenderer spr;
	[SerializeField] AnimationCurve opacityOverLife;
	float startTime;


	void Awake() {
		lifeTime = GetComponent<SelfDestruct> ().delay;
		spr = GetComponent<SpriteRenderer> ();
		startTime = Time.time;
	}

	void Update() {
		spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, opacityOverLife.Evaluate((Time.time - startTime) / lifeTime));
	}
}
