using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/*[BoltGlobalBehaviour(BoltNetworkModes.Host, "CNQ_Map01", 
                     "CNQ_WinterCastle", 
                     "CTF_BridgesOfFate", 
                     "CTF_Map01", 
                     "CTF_WinterCastle", 
                     "Map01", 
                     "WinterCastle")]*/

public class ServerDeathCallbacks : Bolt.GlobalEventListener {

	List<MultikillTimer> killTimers;
	bool firstBlood = false;

	void Awake() {
		killTimers = new List<MultikillTimer> ();
	}

	/*public override void OnEvent (KillEvent evnt) {
		BoltNetwork.Instantiate ((GameObject)Resources.Load("NetworkPrefabs/DeadTank"), evnt.DeathLocation, Quaternion.identity);

		if (evnt.Killer != new Bolt.NetworkId ()) {

			MultikillTimer killerTimer = MultikillTimer.ResetKillTimer (evnt.Killer, killTimers);

			UserDataBlob killer = UserData.GetUserData (evnt.Killer);
			UserDataBlob killed = UserData.GetUserData (evnt.Killed);

			KillNotificationEvent kne = KillNotificationEvent.Create (Bolt.GlobalTargets.Everyone);
			kne.Killer = killer.state.Name;
			kne.Killed = killed.state.Name;

			if (firstBlood == false) {
				kne.AnnouncerSound = "first_blood";
				kne.KillNotificationPrefab = "Kill_FB";
				firstBlood = true;

			} else if (killerTimer.Resets > 0 && killerTimer.Resets < 8) {
				kne.AnnouncerSound = MultiKillSound (killerTimer.Resets);
				kne.KillNotificationPrefab = MultiKillPrefab (killerTimer.Resets);

			} else if (killer.state.KillStreak >= 3 && killer.state.KillTimer == 0) {
				kne.AnnouncerSound = KillingStreakSound (killed.state.KillStreak);
				kne.KillNotificationPrefab = "Kill_1";

			} else {
				kne.AnnouncerSound = "Wrecker";
				kne.KillNotificationPrefab = "Kill_2";
			}

			kne.Send ();
		}
	}*/

	string MultiKillSound(int kills) {
		
		switch (kills) {
		case 1: return "2 Double_Kill";
		case 2: return "3 MultiKill";
		case 3: return "4 MegaKill";
		case 4: return "5 UltraKill";
		case 5: return "6 MonsterKill_F";
		case 6: return "7 LudicrousKill_F";
		case 7: return "8 HolyShit_F";
		}

		return "Wrecker";
	}

	string MultiKillPrefab (int kills) {
		
		switch (kills) {
		case 1: return "Kill_X2";
		case 2: return "Kill_X3";
		case 3: return "Kill_X4";
		case 4: return "Kill_X5";
		case 5: return "Kill_X6";
		case 6: return "Kill_X7";
		case 7: return "Kill_X8";
		}
		
		return "Kill_X2";
	}

	string KillingStreakSound (int kills) {

		switch (kills) {
		case 3: return "3 Killing_Spree";
		case 4: return "4 GodLike";
		case 5: return "5 Ownage";
		case 6: return "6 Rampage";
		case 7: return "7 Unstoppable";
		}

		return "7 Unstoppable";
	}
}

/*[BoltGlobalBehaviour("CNQ_Map01", 
                     "CNQ_WinterCastle", 
                     "CTF_BridgesOfFate", 
                     "CTF_Map01", 
                     "CTF_WinterCastle", 
                     "Map01", 
                     "WinterCastle")]*/
public class DeathCallbacks : Bolt.GlobalEventListener {

	/*public override void OnEvent(KillEvent evnt) {

		UserDataBlob killer = UserData.GetUserData (evnt.Killer);
		UserDataBlob killed = UserData.GetUserData (evnt.Killed);

		if (evnt.FromSelf) {
			killed.state.Deaths ++;
			killed.state.KillStreak = 0;
		}*/

		/*if (evnt.Killer == GameMaster.gm.LocalUserData) {
			killer.state.Kills ++;
			killer.state.KillStreak ++;
		}
	}*/
}

/*[BoltGlobalBehaviour("CNQ_Map01", 
                     "CNQ_WinterCastle", 
                     "CTF_BridgesOfFate", 
                     "CTF_Map01", 
                     "CTF_WinterCastle", 
                     "Map01", 
                     "WinterCastle")]*/
public class AnnouncerCallbacks : Bolt.GlobalEventListener {
	public override void OnEvent (KillNotificationEvent evnt) {

		if (evnt.KillNotificationPrefab != null) {
			try {
				GameObject kn = Instantiate (Resources.Load ("LocalPrefabs/UIMessages/" + evnt.KillNotificationPrefab)) as GameObject;
				kn.transform.SetParent (GameObject.Find ("Canvas").transform, false);
				kn.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -60);

				if(evnt.Killer == "" || evnt.Killer == null) {
					kn.transform.Find("LeftPanel").gameObject.SetActive(false);
				} else {
					kn.transform.Find ("LeftPanel/Text").GetComponent<Text> ().text = evnt.Killer;
				}
			
				if(evnt.Killed == "" || evnt.Killed == null) {
					kn.transform.Find("RightPanel").gameObject.SetActive(false);
				} else {
					kn.transform.Find ("RightPanel/Text").GetComponent<Text> ().text = evnt.Killed;
				}
	
				Destroy (kn, 3);
				}
			catch {
				Debug.LogError("Prefab: " + evnt.KillNotificationPrefab + " could not be instantiated.");
			}
		} else {
			Debug.LogWarning("No notification prefab!");
		}

		if (evnt.AnnouncerSound != null) {
			try {
				//GameMaster.gm.PlayAnnouncerSound (evnt.AnnouncerSound);
			} catch {
				Debug.LogError("Sound: " + evnt.AnnouncerSound + " failed to play.");
			}
		} else {
			Debug.LogWarning("No announcer sound");
		}
	}
}
