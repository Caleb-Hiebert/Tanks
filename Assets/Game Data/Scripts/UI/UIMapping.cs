using UnityEngine;
using System.Collections;

public class UIMapping : MonoBehaviour {

    public Transform local;
    public RectTransform ui;

    public Vector3 offset;
	
	void LateUpdate () {
        if (local == null || ui == null)
            return;

        ui.anchoredPosition = GUIUtility.ScreenToGUIPoint(Camera.main.WorldToScreenPoint(local.position + offset));
	}
}
