using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserInfoPanel : MonoBehaviour {
		
	static UserInfoPanel uip = null;
	public Text kills;
	public Text deaths;
	public Text assists;

	void Awake() {
		uip = this;
	}

	public static string KillDisplay {
		set {
            if (uip == null)
                return;

            uip.kills.text = value;
        }
	}

	public static string DeathDisplay {
		set {
            if (uip == null)
                return;

            uip.deaths.text = value; }
	}

	public static string AssistDisplay {
		set {
            if (uip == null)
                return;

            uip.assists.text = value; }
	}
}
