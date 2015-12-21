using UnityEngine;
using System.Collections;

public class MenuCallbacks : Bolt.GlobalEventListener {

	public MenuUIManager menuGUI;

	public override void ConnectFailed(UdpKit.UdpEndPoint endpoint, Bolt.IProtocolToken token) {
		menuGUI.ConnectionStatusText.text = "Connection Failed";
		menuGUI.LoadingWheel.enabled = false;
	}

	public override void ZeusConnected (UdpKit.UdpEndPoint endpoint) {
		if (BoltNetwork.isServer) {
			BoltNetwork.SetHostInfo ("YES", null);
		} else {
			InvokeRepeating ("SpammyRefresh", 0, 0.75f);
		}
	}

	public void Quit() {
		Application.Quit ();
	}

	void SpammyRefresh() {
		Bolt.Zeus.RequestSessionList ();
		
		IEnumerator list = BoltNetwork.SessionList.GetEnumerator ();
		
		while (list.MoveNext()) {
			Debug.Log((UdpKit.UdpSession)list.Current);
		}
	}
}