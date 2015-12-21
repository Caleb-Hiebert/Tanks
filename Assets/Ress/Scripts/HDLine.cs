using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class HDLine : MonoBehaviour {

    public Vector2 origin;
    public Vector2 endPoint;
    public float z;
    public int lineResolution;

    public LineRenderer lr;

    void LateUpdate()
    {
        lineResolution = Mathf.Clamp(lineResolution, 2, 8000);

        lr = GetComponent<LineRenderer>();

        lr.SetPosition(0, origin);
        lr.SetPosition(1, endPoint);

        lr.SetVertexCount(lineResolution);
        lr.SetPosition(0, origin);

        for (int i = 0; i < lineResolution - 1; i++)
        {
            lr.SetPosition(i, Vector2.Lerp(origin, endPoint, i * (1 / (float)lineResolution)));
        }

        lr.SetPosition(lineResolution - 1, endPoint);
    }

    public GameObject SetData(Vector2 orig, Vector2 endPoint)
    {
        this.origin = orig;
        this.endPoint = endPoint;

        return gameObject;
    }
}
