using UnityEngine;
using System.Collections;

public class GeneralData : MonoBehaviour {

    public static GeneralData data;

    public GameObject[] gameObjects;

	void Awake () {
        data = this;
	}
}
