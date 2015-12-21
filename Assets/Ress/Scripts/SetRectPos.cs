using UnityEngine;
using System.Collections;

public class SetRectPos : MonoBehaviour {

	public float X;
	public float Y;
	private RectTransform rTransform;

	void Awake() {
		rTransform = GetComponent<RectTransform> ();
	}
	
	void Start () {
		rTransform.anchoredPosition = new Vector2 (X, Y);
	}
}
