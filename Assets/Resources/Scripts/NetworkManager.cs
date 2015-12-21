using UnityEngine;
using System;
using System.Collections;
using UdpKit;

public class NetworkManager : Bolt.GlobalEventListener {

	public MenuManager menuGUI;
	public GameData.Maps map;
    public GameData.GameModes gameMode;
	public ushort port = 27000;
    public string address = "127.0.0.1";
    public string serverName = "Unnamed Server";
    public static string playerName;

    public int state;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

	public void StartClient() {
        StartCoroutine(ServerSelection());
	}

	public void HostServer() {
        StartCoroutine(Host());
	}

    IEnumerator Host()
    {
        BoltLauncher.StartServer(port);
        state = ClientStates.HostServer;
        yield return null;
    }

    IEnumerator ServerSelection()
    {
        BoltLauncher.StartClient();
        state = ClientStates.GetServerList;
        yield return null;
    }

	public override void BoltStartDone () {

        BoltNetwork.RegisterTokenClass<ConnectToken>();
        BoltNetwork.RegisterTokenClass<GrappleToken>();
        BoltNetwork.RegisterTokenClass<ServerInfoToken>();

        if(state == ClientStates.GetServerList)
        {

        } else if (state == ClientStates.HostServer)
        {
            LoadMap();
        }
	}

    public override void ZeusConnected(UdpEndPoint endpoint)
    {
        if (BoltNetwork.isServer)
        {
            var t = new ServerInfoToken();
            t.mapInfo = GameData.GetMapSceneName(map, gameMode);
            BoltNetwork.SetHostInfo(serverName, t);
        }
    }

    public static void Connect(UdpSession point)
    {
        var cToken = new ConnectToken();
        cToken.name = playerName;

        BoltNetwork.Connect(point, cToken);
    }

    public void Connect()
    {
        var cToken = new ConnectToken();
        cToken.name = (playerName == null || playerName == "") ? Utils.GetRandomName() : playerName;

        BoltNetwork.Connect(UdpEndPoint.Parse(address + ":" + port), cToken);
    }

    public void LoadMap() {
        BoltNetwork.LoadScene("LobbyScene");
	}

    public string ServerName
    {
        set { serverName = value; }
    }

    public string IP
    {
        set { address = value; }
    }
}

class ClientStates
{
    public static readonly int GetServerList = 0;
    public static readonly int HostServer = 1;
}
