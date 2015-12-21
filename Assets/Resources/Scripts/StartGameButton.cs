using UnityEngine;
using System.Collections;

public class StartGameButton : MonoBehaviour {
	
	public void StartGame() {
		if (BoltNetwork.isServer) {
			BoltNetwork.LoadScene("WinterCastle");
		}
	}
}
