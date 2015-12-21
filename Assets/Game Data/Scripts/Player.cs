using UnityEngine;
using System.Collections;

public class Player {

    public BoltEntity character = null;
    public BoltConnection connection;

    public static int localTeam;

    static BoltEntity localPlayer;

    public bool isServer
    {
        get { return connection == null; }
    }

    public bool isClient
    {
        get { return connection != null; }
    }

    public void Spawn()
    {
        if (character == null)
        {
            character = BoltNetwork.Instantiate(BoltPrefabs.Player);

            ConnectToken t = null;

            if (connection != null)
            {
                t = (ConnectToken)connection.ConnectToken;

                if (t != null)
                    character.GetState<IPlayer>().Name = t.name;
            } else
            {
                character.GetState<IPlayer>().Name = (NetworkManager.playerName == null || NetworkManager.playerName == "") ? Utils.GetRandomName() : NetworkManager.playerName;
            }
            
            if (isServer)
            {
                character.TakeControl();
            }
            else
            {
                character.AssignControl(connection);
            }
        }
    }

    public void OnDisconnect()
    {
        BoltNetwork.Destroy(character);
    }

    public static BoltEntity LocalPlayer {
        get
        {
            return localPlayer;
        }

        set
        {
            localPlayer = value;
        }
    }

    public static IPlayer LocalPlayerState
    {
        get {
            if (localPlayer == null)
                return null;

            return LocalPlayer.GetState<IPlayer>();
        }
    }
}
