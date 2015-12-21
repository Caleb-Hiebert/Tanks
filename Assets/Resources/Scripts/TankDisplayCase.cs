using UnityEngine;
using System.Collections;

public class TankDisplayCase : MonoBehaviour {

	public string[] availableTanks;
	public GameObject platter;
	public MenuUIManager menuGUI;

	private GameObject currentDisplayTank;
	private int displayedTank = 0;

	void Start() {
		DisplayTank (availableTanks [0]);
	}

	public void SelectNext() {
		if (displayedTank < availableTanks.Length - 1) {
			displayedTank++;
			DisplayTank (availableTanks[displayedTank]);
		} else {
			displayedTank = 0;
			DisplayTank(availableTanks[displayedTank]);
		}
	}

	public void SelectPrevious() {
		if (displayedTank > 0) {
			displayedTank--;
			DisplayTank (availableTanks[displayedTank]);
		} else {
			displayedTank = availableTanks.Length - 1;
			DisplayTank(availableTanks[displayedTank]);
		}
	}

	void DisplayTank(string tankName) {
		Destroy (currentDisplayTank);
		currentDisplayTank = Instantiate(Resources.Load("MenuTanks/" + tankName), Vector3.zero, Quaternion.identity) as GameObject;
		currentDisplayTank.transform.SetParent(platter.transform);
		currentDisplayTank.transform.position = Vector3.zero;
		menuGUI.SetTankName(tankName);

		//GameMaster.gm.SwapTanks (tankName);
	}

	public string GetChosenTank() {
		return availableTanks [displayedTank];
	}
}
