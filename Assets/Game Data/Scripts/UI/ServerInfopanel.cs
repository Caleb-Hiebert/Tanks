using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ServerInfopanel : MonoBehaviour {

	public Text timer;
	public Text redKills;
	public Text blueKills;

	void Update() {
		if (BoltNetwork.isConnected) {
            timer.text = BoltNetwork.serverTime.FormatSeconds();
		}
	}
}
