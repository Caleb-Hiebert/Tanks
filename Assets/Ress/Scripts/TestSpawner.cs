using UnityEngine;
using System.Collections;

public class TestSpawner : MonoBehaviour {

	public GameObject damageIndicator;

	// Update is called once per frame
	void Update () {
		if (InputHandler.GetMouseDown (0)) {
			Instantiate(damageIndicator, Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, -10), Quaternion.identity);
		}
	}
}
