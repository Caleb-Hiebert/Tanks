using UnityEngine;
using System.Collections;

public class BurnMarks : Bolt.EntityBehaviour<IPositionState> {

	public Sprite[] burnSprites;
	SpriteRenderer spr;

	public override void Attached () {
        state.SetTransforms(state.Transform, transform);
	}

	void Start () {
		spr = GetComponent<SpriteRenderer> ();
		spr.sprite = burnSprites [Random.Range (0, burnSprites.Length - 1)];
	}
}
