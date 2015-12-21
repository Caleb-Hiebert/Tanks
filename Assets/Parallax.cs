using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

    public float amount;

    Vector2 lastCamPos;
	
	void Update () {
        var camPosDelta = (Vector2)Camera.main.transform.position - lastCamPos;

        transform.Translate(camPosDelta * amount);

        lastCamPos = Camera.main.transform.position;
	}
}
