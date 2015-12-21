using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TankUI : MonoBehaviour {
		
	public static TankUI ui;

	public Text lowerText;
	public Text upperText;
	public Text healthText;
	public Text secondaryCooldownText;
	public Image slidingImage;
	public Image healthBar;
	public Image secondaryCooldownBar;
	public GameObject wholeThing;

	void Awake() {
		ui = this;
		gameObject.name = "Canvas";
	}

	public float SliderAmount {
		set { slidingImage.fillAmount = value; }
	}

	public float SecondaryCooldown {
		set { secondaryCooldownBar.fillAmount = value; }
	}


	public string LowerText {
		set { lowerText.text = value; }
	}

	public string SecondaryCooldownText {
		set { secondaryCooldownText.text = value; }
	}

	public string UpperText {
		set { upperText.text = value; }
	}

	public string HealthText {
		set { healthText.text = value; }
	}

	public float HealthBar {
		set { healthBar.fillAmount = value; }
	}

	public bool TurnedOn {
		get { return wholeThing.activeSelf; }
		set { wholeThing.SetActive (value); }
	}

	public void SetToDefault() {
		UpperText = "---";
		LowerText = "---";
		HealthText = "0/0";
		SecondaryCooldownText = "";
	}

	public static string SecondsCooldown(float timeWhenDone) {
		int s = Mathf.RoundToInt (timeWhenDone - Time.time);
		if (s < 0) {
			return "|||";
		}

		return s + "s";
	}

	public static float SliderPercent(float timeStarted, float timeWhenDone) {
		return (timeStarted + Time.time) / timeWhenDone;
	}
}
