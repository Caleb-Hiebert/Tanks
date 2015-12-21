using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

    public static GameCamera gc;

    public Transform target;
    public float lerpSpeed;
    public bool lerpFollow;
    public float arrowKeySpeed;
    public float zValue = -10;

    void Awake()
    {
        gc = this;
    }

    void Update()
    {
        if(target != null)
        {
            var t = target.position;
            t.z = zValue;

            if (lerpFollow)
            {
                transform.position = Vector3.Lerp(transform.position, t, Time.deltaTime * lerpSpeed);
            } else
            {
                transform.position = t;
            }
        } else
        {
            transform.Translate(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * arrowKeySpeed * -1, Input.GetAxis("Vertical") * Time.deltaTime * arrowKeySpeed));
        }
    }

    public static GameCamera Get()
    {
        return GameCamera.gc;
    }

    void OnLevelWasLoaded(int level)
    {
        if(level == 0)
        {
            Destroy(gameObject);
        }
    }
}
