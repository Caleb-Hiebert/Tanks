using UnityEngine;
using System.Collections;

public class BridgesRaft : MonoBehaviour {

    Vector2 lastFramePosition, deltaMovement;
    public float radius;

	void Start () {
        lastFramePosition = transform.position;
	}
	
	void FixedUpdate () {
        deltaMovement = (Vector2)transform.position - lastFramePosition;
        lastFramePosition = transform.position;

        foreach (var item in Physics2D.OverlapCircleAll(transform.position, radius))
        {
            var entity = item.GetComponentInParent<BoltEntity>();

            if (entity != null)
            {
                entity.transform.Translate(-1 * deltaMovement);
            }
        }
	}
}
