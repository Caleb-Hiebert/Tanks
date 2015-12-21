using UnityEngine;
using System.Collections;

public class EventHandler : Bolt.GlobalEventListener {
	
	public delegate void TankAttached(GameObject tank, Bolt.NetworkId owner);
	public static event TankAttached TankSpawned;

	public delegate void TankDetached(BoltEntity tank, Bolt.NetworkId owner);
	public static event TankDetached TankDespawned;

	public delegate void GameOverEvent(int winningTeam);
	public static event GameOverEvent GameOver;

	/*public delegate void PlayerJoinEvent(string playerName, UserDataBlob udb);
	public static event PlayerJoinEvent PlayerJoinedGame;*/

	/*public delegate void PlayerLeaveEvent (string playerName, UserDataBlob udb);
	public static event PlayerLeaveEvent PlayerLeftGame;*/

	public static void TriggerGameOver(int winningTeam) {
		if (GameOver != null) {
			GameOver(winningTeam);
		}
	}

	/*public override void EntityAttached (BoltEntity entity) {
		if (entity.StateIs<ITankState> ()) {

			var tankToken = (TankSpawnToken) entity.attachToken;
			Bolt.NetworkId owner = tankToken.owner;

			if(TankSpawned != null) {
				TankSpawned(entity.gameObject, owner);
			}
		} else if (entity.StateIs<IPlayerData>()) {
			if(PlayerJoinedGame != null) {
				PlayerJoinedGame(((UserDataToken) entity.attachToken).name, entity.GetComponent<UserDataBlob>());
			}
		}
	}

	public override void EntityDetached (BoltEntity entity) {
		if (entity.StateIs<IPlayerData> ()) {

			if (PlayerLeftGame != null) {
				PlayerLeftGame (((UserDataToken)entity.attachToken).name, entity.GetComponent<UserDataBlob> ());
			}
		} else if (entity.StateIs<IPlayerData> ()) {
			if(TankDespawned != null) {
				TankDespawned(entity, entity.GetComponent<TankNetworkController>().userData.entity.networkId);
			}
		}
	}*/
}
