using UnityEngine;
using System.Collections;

//[BoltGlobalBehaviour("LobbyScene")]
public class LobbyCallbacks : Bolt.GlobalEventListener {
		
	/*public override void SceneLoadLocalDone (string map) {
		ChatMessageEvent cme = ChatMessageEvent.Create (Bolt.GlobalTargets.Everyone);
		cme.Message = ((UserDataToken)GameMaster.gm.userData.entity.attachToken).name + " connected to chat.";
		cme.Send ();
	}*/

}

//[BoltGlobalBehaviour(BoltNetworkModes.Host, "LobbyScene")]
public class ServerLobbyCallbacks : Bolt.GlobalEventListener {
}
