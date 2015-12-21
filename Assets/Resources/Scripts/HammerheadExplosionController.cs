using UnityEngine;
using System.Collections.Generic;

public class HammerheadExplosionController : MonoBehaviour {

    [Range(3, 360)]
    public int rays;

    public float range, angle;

    public Vector3 rayPoint;

    void Start()
    {
        List<BoltEntity> hits = new List<BoltEntity>();

        for (int i = 0; i < rays; i++)
        {
            var hit = Physics2D.Raycast(rayPoint, Quaternion.Lerp(Quaternion.Euler(0, 0, -angle), Quaternion.Euler(0, 0, angle), (float)i / (float)rays).ToVector(), range, ClientCallbacks.mask);

            if (hit.collider != null && hit.collider.tag == "Player")
            {
                hits.Add(hit.collider.GetComponentInParent<BoltEntity>());

                Debug.Log(hits[i].GetState<IPlayer>().Name);

                Debug.DrawLine(rayPoint, hit.point, Color.green, 5f);
            }
        }
    }
}
