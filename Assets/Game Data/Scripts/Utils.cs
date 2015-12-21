using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utils : MonoBehaviour {

	public static float PositiveDifference(float a, float b) {
		float res = a - b;
		
		if (res > 0)
			return res;
		else
			return res * -1;
	}

	public static float Positivize(float flin) {
		if (flin < 0)
			return flin * -1;
		else
			return flin;
	}

	public static bool WithinRange(float value, float checkValue, float range) {
		return (value > checkValue - range && value < checkValue + range);
	}

	public static void DrawLaser(Vector3 start, Vector3 end, float displayTime) {
		GameObject trail = Instantiate (Resources.Load("LocalPrefabs/Laser")) as GameObject;
		LineRenderer lr = trail.GetComponent<LineRenderer> ();
		lr.SetPosition (0, start);
		lr.SetPosition (1, end);
		Destroy (trail, displayTime);
	}

	public static void DrawZapLaser (Vector3 start, Vector3 end, float displayTime) {
		GameObject trail = Instantiate (Resources.Load("LocalPrefabs/Laser")) as GameObject;
		LineRenderer lr = trail.GetComponent<LineRenderer> ();
		lr.SetWidth (1, 0.2f);
		lr.SetColors (Color.red, Color.red);
		lr.SetPosition (0, start);
		lr.SetPosition (1, end);
		Destroy (trail, displayTime);
	}

	public static float Remap (float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

    public static GameObject[] GetPlayersInRange(Vector2 startingPoint, float range) {
		Collider2D[] objects = Physics2D.OverlapCircleAll (startingPoint, range);

		List<GameObject> playerList = new List<GameObject> ();

		foreach (Collider2D c in objects) {
			if(c.tag == "Player") {
				playerList.Add(c.gameObject);
			}
		}

		GameObject[] players = new GameObject[playerList.Count];

		for (int i = 0; i < players.Length; i ++) {
			players[i] = playerList[i];
		}

		return players;
	}

	public static string GetRandomName() {
		string[] randNames = new string[20] {"( ͡° ͜ʖ ͡°)", 
			"( ͠° ͟ʖ ͡°)", 
			"( ͡~ ͜ʖ ͡°)", 
			"( ͡o ͜ʖ ͡o)", 
			"͡° ͜ʖ ͡ -", 
			"( ͡͡ ° ͜ ʖ ͡ °)", 
			"( ͡ ͡° ͡°  ʖ ͡° ͡°)", 
			"(ง ͠° ͟ل͜ ͡°)",
			"( ͡° ͜ʖ ͡ °)", 
			"(ʖ ͜° ͜ʖ)", 
			"( ͡o ͜ʖ ͡o)", 
			"( ‾ʖ̫‾)", 
			"( ͡°╭͜ʖ╮͡° )" ,
			"( ͡°╭͜ʖ╮͡° )" ,
			"ʕ•́ᴥ•̀ʔっ" ,
			"(̶◉͛‿◉̶)" ,
			"( ˘︹˘ )" ,
			"(⊙.⊙(◉̃_᷅◉)⊙.⊙)" ,
			"(͠◉_◉᷅ )" ,
			"ヽ(ｏ`皿′ｏ)ﾉ"};

		return randNames[Random.Range(0, randNames.Length - 1)];
	}
}
