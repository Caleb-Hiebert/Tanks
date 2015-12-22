using UnityEngine;
using System.Collections;

public class DamageIndicator : MonoBehaviour {

	public static GameObject diPrefab;
	public float lifeTime;
	public TextMesh text;
	private float alpha = 1;
	private Vector3 movementVector;

	void Start() {
		Destroy (this.gameObject, lifeTime);
		movementVector = new Vector3 (Random.Range (-0.5f, 0.5f), Random.Range (10, 5), 0);
		try{
			int amnt = int.Parse (text.text);
			transform.localScale = new Vector3 (Mathf.Clamp(0.01f * Utils.Positivize(amnt), 0.7f, 1.3f), Mathf.Clamp(0.01f * Utils.Positivize(amnt), 0.7f, 1.3f), 1);
		} catch {
			transform.localScale = new Vector3 (1, 1, 1);
		}
	}

	void Update() {
		transform.position = transform.position + movementVector * Time.deltaTime;
		text.color = new Color (text.color.r, text.color.g, text.color.b, alpha -= (1f * Time.deltaTime));
	}

	public static void SpawnDamageIndicator(Vector2 pos, int amount) {

		if (amount == 0 || !UserSettings.damageIndicators) {
			if(amount == 0) Debug.LogWarning("Damage indicator was 0, returning");
			if(!UserSettings.damageIndicators) { 
				Debug.LogWarning("Damage indicators are " + UserSettings.damageIndicators + ", returning");
			}

			return;
		}

		if (diPrefab == null) {
            diPrefab = GeneralData.data.gameObjects.Get("DamageIndicator");
		}
		
		GameObject di = Instantiate (diPrefab, pos, Quaternion.identity) as GameObject;
		
		if (amount > 0) {
			
			di.GetComponent<TextMesh>().text = "+" + amount;
			di.GetComponent<TextMesh>().color = Color.green;
			
		} else { 
			di.GetComponent<TextMesh>().text = "" + amount;
			di.GetComponent<TextMesh>().color = Color.red;
		}
	}

	public static void SpawnTextIndicator(Vector2 pos, string text, Color color) {
		
		if (diPrefab == null) {
			diPrefab = (GameObject)Resources.Load("LocalPrefabs/DamageIndicator");
		}
		
		GameObject di = Instantiate (diPrefab, pos, Quaternion.identity) as GameObject;

		di.GetComponent<TextMesh>().text = text;
		di.GetComponent<TextMesh>().color = color;
	}
}
