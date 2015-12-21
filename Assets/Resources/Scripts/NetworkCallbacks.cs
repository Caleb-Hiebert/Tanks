using UnityEngine;
using System.Collections;
using Bolt;
using UdpKit;

[BoltGlobalBehaviour(BoltNetworkModes.Host)]
public class NetworkCallbacks : GlobalEventListener
{
    void Awake()
    {
        PlayerRegistry.CreateServerPlayer();
    }

    public override void Connected(BoltConnection arg)
    {
        PlayerRegistry.CreateClientPlayer(arg);
    }

    public override void ConnectRequest(UdpEndPoint endpoint, IProtocolToken token)
    {
        ConnectToken cToken = (ConnectToken)token;

        BoltNetwork.Accept(endpoint, cToken);
    }

    public override void SceneLoadLocalDone(string map)
    {
        PlayerRegistry.serverPlayer.Spawn();
        BoltNetwork.Instantiate(BoltPrefabs.GameData);
    }

    public override void SceneLoadRemoteDone(BoltConnection connection)
    {
        PlayerRegistry.GetPlayer(connection).Spawn();
    }

    public override void Disconnected(BoltConnection connection)
    {
        PlayerRegistry.GetPlayer(connection).OnDisconnect();
    }

    public override void OnEvent(SpawnRequest evnt)
    {
        PlayerRegistry.GetPlayer(evnt.RaisedBy).character.GetComponent<PlayerController>().Kill();
    }

    public override void OnEvent(StateChangeEvent evnt)
    {
        var player = PlayerRegistry.GetPlayer(evnt.RaisedBy).character;
        var state = player.GetState<IPlayer>();

        state.State.ChosenSkin = evnt.Skin;
        state.State.ChosenState = evnt.State;        

        player.GetComponent<PlayerController>().Kill();
    }

    void Update()
    {
        if(InputHandler.GetKeyDown(KeyCode.L))
        {
            BoltNetwork.LoadScene("LobbyScene");
        }
    }
}
