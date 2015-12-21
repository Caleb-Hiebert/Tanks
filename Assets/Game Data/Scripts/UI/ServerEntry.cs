using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class ServerEntry : MonoBehaviour {
    
    public static List<ServerEntry> serverList = new List<ServerEntry>();

    static float height = 25;

    public UdpKit.UdpSession session;

    public Text hostName, ip, players, mapInfo;

    void Awake()
    {
        serverList.Add(this);
    }

    void Start()
    {
        UpdateList();
    }

    public void Connect()
    {
        NetworkManager.Connect(session);
    }

    void UpdateList()
    {
        if (session == null)
            return;

        hostName.text = session.HostName;
        ip.text = session.WanEndPoint.Address.ToString();
        players.text = string.Format("{0}/{1}", session.ConnectionsCurrent, session.ConnectionsMax);

        var t = (ServerInfoToken)session.GetProtocolToken();
        if(t != null)
            
            mapInfo.text = t.mapInfo;
    }

    public static void Organize()
    {
        for (int i = 0; i < serverList.Count; i++)
        {
            var e = serverList[i];

            e.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * height);
        }
    }
}
