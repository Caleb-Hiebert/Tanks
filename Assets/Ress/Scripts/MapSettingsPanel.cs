using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapSettingsPanel : MonoBehaviour {
		
	public InputField[] options;

	public string GetOption(string optionName) {

		for(int i = 0; i < options.Length; i++) {
			if(options[i].name == optionName) return options[i].text;
		}

		return "0";
	}
}
