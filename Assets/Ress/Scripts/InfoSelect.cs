using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoSelect : MonoBehaviour {
	
	public Text mapName;
	public Image mapDisplay;
	public GameObject vanillaPanel;
	public GameObject conquestPanel;
	public GameObject ctfPanel;
	public Sprite[] mapPictures;

	void Start() {
		ChangeMap ("WinterCastle");
	}

	public void ChangeMap(string mapName) {
		this.mapName.text = mapName;
		//NetworkManager.nm.defaultMap = mapName;
		mapDisplay.sprite = mapPictures [GetMap (mapName)];

		SetPanel (mapName);
	}

	void SetPanel(string mapName) {

		switch (Utils.GetGameMode (mapName)) {
		case "CNQ": vanillaPanel.SetActive(false);
			ctfPanel.SetActive(false);
			conquestPanel.SetActive(true);
			break;

		case "CTF": vanillaPanel.SetActive(false);
			ctfPanel.SetActive(true);
			conquestPanel.SetActive(false);
			break;
		

		case "Vanilla": vanillaPanel.SetActive(true);
			ctfPanel.SetActive(false);
			conquestPanel.SetActive(false);
			break;
		}
	}

	int GetMap(string mapName) {
		for (int i = 0; i < mapPictures.Length; i ++) {
			Debug.Log(mapPictures[i].name);
			if(mapPictures[i].name == mapName) {
				return i;
			}
		}

		return 0;
	}


}
