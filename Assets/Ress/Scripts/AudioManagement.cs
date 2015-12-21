using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;

public class AudioManagement : MonoBehaviour {

	public AudioMixer master;

	void Update() {
		SetAmbientVolume (UserSettings.ambientVolume);
		SetSFXVolume (UserSettings.sfxVolume);
	}

	public void SetAmbientVolume (float vol) {
		master.SetFloat ("Ambient", vol);
	}

	public void SetSFXVolume (float vol) {
		master.SetFloat ("SFX", vol);
	}

	public void SetMasterVolume (float vol) {
		master.SetFloat ("Master", vol);
	}
}
