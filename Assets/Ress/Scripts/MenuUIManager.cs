using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour {

	public Button hostButton;
	public Button joinButton;
	public Button optionsButton;
	public InputField nameInput;
	public InputField ipInput;
	public InputField portInput;
	public Text tankNameDisplay;
	public Image loadingWheel;
	public Text connectionStatusText;
	public Toggle teamToggle;


	public string GetUserName() {
		if (nameInput.text == null || nameInput.text == "") {
			return "default";
		}
		return nameInput.text;
	}

	public string GetIPAdress() {
		if (ipInput.text == null || ipInput.text == "") {
			return "default";
		}
		return ipInput.text;
	}

	public string GetPort() {
		if(portInput.text == null || portInput.text == "") {
			return "default";
		}
		return portInput.text;
	}

	public bool UsingTeams {
		get { return teamToggle.isOn; }
	}

	public void SetTankName(string tankName) {
		tankNameDisplay.text = tankName;
	}

	public Text ConnectionStatusText {
		get {return connectionStatusText;}
	}

	public Image LoadingWheel {
		get {return loadingWheel;}
	}
}
