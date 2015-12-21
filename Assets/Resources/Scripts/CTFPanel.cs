using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CTFPanel : MonoBehaviour {

	public static CTFPanel ctp;
	public Text redFlags;
	public Text blueFlags;
	public GameObject cPanel;

	void Awake() {
		ctp = this;
	}

	public string RedFlags {
		set { redFlags.text = value; }
	}

	public string BlueFlags {
		set { blueFlags.text = value; }
	}

	public void TurnOn() {
		cPanel.SetActive (true);
	}
}
