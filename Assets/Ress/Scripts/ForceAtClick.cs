using UnityEngine;
using System.Collections;

public class ForceAtClick : MonoBehaviour {

    public float dist, force;
    public LayerMask mask;
    public Camera cam;

	void Update () {
	    if(InputHandler.GetMouseDown(0))
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(cam.ScreenToWorldPoint(Input.mousePosition), dist, mask, 10);

            foreach (var item in cols)
            {
                item.GetComponent<Rigidbody2D>().ExplosionForce(dist, force);
            }
        }
	}
}
