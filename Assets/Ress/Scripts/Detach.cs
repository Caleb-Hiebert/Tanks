using UnityEngine;
using System.Collections;

public class Detach : MonoBehaviour {

	void Start () {
        transform.SetParent(null);
	}
}
