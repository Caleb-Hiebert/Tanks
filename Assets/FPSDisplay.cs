using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPSDisplay : MonoBehaviour {

    public Text t;

	void Update () {
        t.text = Mathf.RoundToInt(1f / Time.smoothDeltaTime) + " FPS";
    }
}
