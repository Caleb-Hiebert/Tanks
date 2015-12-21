using UnityEngine;
using System.Collections;

public class GetServers : MonoBehaviour {

	public void DisplayServers() {
		IEnumerator list = BoltNetwork.SessionList.GetEnumerator ();

		while (list.MoveNext()) {
			Debug.Log("Hpera Derpa");
		}
	}

	public void UpdateServers() {
		Bolt.Zeus.RequestSessionList ();
	}
}
