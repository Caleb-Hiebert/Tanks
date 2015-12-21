using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LobbyController : MonoBehaviour {

    NetworkManager nm;

    public Dropdown mode, map;

    public Button startGame;

    void Awake () {
        nm = GameObject.Find("_GameMaster").GetComponent<NetworkManager>();

        mode.interactable = BoltNetwork.isServer;
        map.interactable = BoltNetwork.isServer;
        startGame.interactable = BoltNetwork.isServer;
    }

    public void StartGame()
    {
        StartCoroutine(StartGame(5f));
    }

    public void SelectMode(int m)
    {
        var md = mode.options[m].text;

        switch(md)
        {
            case "Team Deathmatch": nm.gameMode = GameData.GameModes.TeamDeathmatch;
                break;
            case "Capture the Flag": nm.gameMode = GameData.GameModes.CaptureTheFlag;
                break;
            case "Point Capture": nm.gameMode = GameData.GameModes.PointCapture;
                break;
            case "Last Man Standing": nm.gameMode = GameData.GameModes.LastManStanding;
                break;
            case "Free For All": nm.gameMode = GameData.GameModes.FreeForAll;
                break;
        }

        ChatCore.SendServerMessage("New Map: " + GameData.GetMapSceneName(nm.map, nm.gameMode));
    }

    public void SelectMap(int m)
    {
        var md = map.options[m].text;

        switch(md)
        {
            case "Map01": nm.map = GameData.Maps.Map01;
                break;
            case "Winter Castle": nm.map = GameData.Maps.WinterCastle;
                break;
            case "Bridges": nm.map = GameData.Maps.Bridges;
                break;
        }

        ChatCore.SendServerMessage("New Map: " + GameData.GetMapSceneName(nm.map, nm.gameMode));
    }

    IEnumerator StartGame (float delay)
    {
        while(delay > 0)
        {
            ChatCore.SendServerMessage("Game Starting in " + delay);
            yield return new WaitForSeconds(1);
            delay--;
        }

        BoltNetwork.LoadScene(GameData.GetMapSceneName(nm.map, nm.gameMode));
    }
}
