using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hitmarker : MonoBehaviour {

	private Image img;
	public float hitMarkerFadeTime = 1;
	private float alpha = 0;
	
	void Start () {
		img = GetComponent<Image> ();
		img.color = new Color (255, 255, 255, alpha);
	}

	void Update() {
		alpha -= hitMarkerFadeTime * Time.deltaTime;

		if (alpha < 0)
			alpha = 0;

		img.color = new Color (1, 1, 1, alpha);
	}

	public void Hit() {
		alpha = 1;
	}
}
