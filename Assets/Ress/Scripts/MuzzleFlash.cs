using UnityEngine;
using System.Collections;

public class MuzzleFlash : MonoBehaviour {

    public Vector2 origin;
    public float angle;

	private SpriteRenderer spr;
	public float flashDisplayTime = 0.01f;
	private float flashDisplayTimer = 0;

	// Use this for initialization
	void Start () {
		spr = GetComponentInChildren<SpriteRenderer> ();

        transform.position = origin;
        transform.rotation = Quaternion.Euler(0, 0, angle);
	}
	
	// Update is called once per frame
	void Update () {
		if(spr.enabled == true) flashDisplayTimer += Time.deltaTime;

		if (flashDisplayTimer >= flashDisplayTime) {
			spr.enabled = false;
		}
	}

    public void SetData(Vector2 orig, float angle)
    {
        this.origin = orig;
        this.angle = angle;
    }
}
