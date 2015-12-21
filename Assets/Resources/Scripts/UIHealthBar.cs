using UnityEngine;
using System.Collections;

public class UIHealthBar : MonoBehaviour {

	public GameObject tank;
	public RectTransform rTransform;

	public void SetInfo(GameObject tank) {
		this.tank = tank;
	}

	void LateUpdate () {
		if (tank != null) {
			try {
				Vector3 pos = Camera.main.WorldToScreenPoint (tank.transform.position);
				rTransform.anchoredPosition = new Vector2 (pos.x, pos.y + 30);
			} catch {
				Debug.LogWarning("Camera not found.");
			}
		} else {
			Destroy(this.gameObject);
		}
	}
}
