using UnityEngine;
using System.Collections;

public class ToMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    if(BoltNetwork.isConnected)
        {
            Destroy(gameObject);
        } else
        {
            Application.LoadLevel("StartScene");
        }
	}
}
