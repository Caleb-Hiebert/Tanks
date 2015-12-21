using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EscapeMenu : MonoBehaviour {
		
	public static EscapeMenu em;

	public GameObject escapeMenu, tankSelection;
	public Button switchTeamsButton;
	public Slider cameraShake;
	public Slider sfxVolume;
	public Slider ambientVolume;

	private bool active;

	void Awake() {
		em = this;
	}

	void Start() {
		cameraShake.value = UserSettings.cameraShake;
		sfxVolume.value = UserSettings.sfxVolume;
		ambientVolume.value = UserSettings.ambientVolume;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
            if (InputHandler.CanEscapeMenu())
            {
                MenuEnabled = !MenuEnabled;
                active = escapeMenu.activeSelf;
            }
		}
	}

	public void ReturnToMenu() {
        BoltLauncher.Shutdown();
	}

	public void Spectate() {
        MenuEnabled = false;
        active = escapeMenu.activeSelf;
	}


	public void Play() {
		
	}

    public void SelectTank()
    {
        tankSelection.SetActive(true);
        MenuEnabled = false;
        active = escapeMenu.activeSelf;
    }

	public bool MenuEnabled { 
		get { return active; }
		set { escapeMenu.SetActive (value);
            if(!escapeMenu.activeSelf)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
	}

	public void SetCamShake(float factor) {
		UserSettings.cameraShake = factor;
	}

	public void SetSFXVolume (float factor) {
		UserSettings.sfxVolume = factor;
	}

	public void SetAmbientVolume (float factor) {
		UserSettings.ambientVolume = factor;
	}

	public void SetDynamicTerrainQuality(float factor) {
		UserSettings.dynamicTerrainQuality = factor;
	}

	public void Quit() {
		Application.Quit ();
	}
}
