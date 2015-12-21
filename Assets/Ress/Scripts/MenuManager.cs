using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UdpKit;

public class MenuManager : Bolt.GlobalEventListener {

    public Animator cameraAnimator;

    public GameObject serverListContainer;
    public GameObject serverEntryPrefab;

    public InputField nameInput;

    public NetworkManager nm;

    void Awake()
    {
        nm = GameObject.Find("_GameMaster").GetComponent<NetworkManager>();
        NetworkManager.playerName = Utils.GetRandomName();
        nameInput.text = NetworkManager.playerName;
    }

    public void StartServer()
    {
        nm.HostServer();
    }

    public void Host()
    {
        nm.HostServer();
    }

    public void Join()
    {
        cameraAnimator.Play("JoinAnimation");
        nm.StartClient();
    }

    public void OnHostCancel()
    {
        cameraAnimator.Play("IdleMenu");
    }

    public void SetName(string n)
    {
        NetworkManager.playerName = n;
    }

    public void UpdateServerDisplay()
    {
        ClearList();

        foreach (var item in BoltNetwork.SessionList)
        {
            if (item.Value == null)
                return;

            GameObject ne = Instantiate(serverEntryPrefab);
            ne.transform.SetParent(serverListContainer.transform, false);
            ne.GetComponent<ServerEntry>().session = item.Value;
        }
    }

    public override void ZeusConnected(UdpEndPoint endpoint)
    {
        InvokeRepeating("RefreshServerList", 0.1f, 1.0f);
    }

    void RefreshServerList()
    {
        Bolt.Zeus.RequestSessionList();

        UpdateServerDisplay();
    }

    void OnDestroy()
    {
        ServerEntry.serverList.Clear();
        CancelInvoke();
    }

    void ClearList()
    {
        foreach (var item in ServerEntry.serverList)
        {
            if(item != null) 
                Destroy(item.gameObject);
        }

        ServerEntry.serverList.Clear();
    }
}
