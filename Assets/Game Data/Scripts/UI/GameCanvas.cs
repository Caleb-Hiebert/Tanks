using UnityEngine;
using System.Collections;

public class GameCanvas : MonoBehaviour {

    public static GameCanvas current;

	void Awake () {
        current = this;
	}

    public static RectTransform UITransform {
        get { return current.GetComponent<RectTransform>(); }
    }
}
