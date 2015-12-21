using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LastGameStatus : MonoBehaviour {

	public GameObject panel;
	public Text panelText;
	
	void Start() {
		InvokeRepeating ("GetInfo", 0, 0.5f);
	}

	void GetInfo() {

		Debug.Log ("looking for info");

		if (GameObject.Find ("RoundInfo") != null) {
			panel.SetActive(true);
            int winningTeam = 0;

			string boxText = "";

			if(winningTeam == 1) {
				boxText = "Red team won the game.";
			} else if (winningTeam == 2) {
				boxText = "Blue team won the game.";
			}

			panelText.text = boxText;

			CancelInvoke("GetInfo");
		}
	}
}
