using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CNQPanel : MonoBehaviour {

	public static CNQPanel cnq;

	[SerializeField] GameObject panel;
	[SerializeField] Text redPoints;
	[SerializeField] Text bluePoints;
	[SerializeField] Text redPercent;
	[SerializeField] Text bluePercent;

	void Awake() {
		cnq = this;
	}

	public bool TurnedOn {
		set { panel.SetActive(value); }
		get { return panel.activeSelf; }
	}

	public string RedPoints {
		set { redPoints.text = value; }
	}

	public string BluePoints {
		set { bluePoints.text = value; }
	}

	public string RedPercent {
		set { redPercent.text = value + "%"; }
	}
	
	public string BluePercent {
		set { bluePercent.text = value + "%"; }
	}
}
