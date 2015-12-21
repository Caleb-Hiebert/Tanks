using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MapSelection : MonoBehaviour {

    public Button map01, winterCastle, bridgesOfFate, hostButton;

    public Text display;

    public NetworkManager nm;

    void Awake()
    {
        nm = GameObject.Find("_GameMaster").GetComponent<NetworkManager>();
        display.text = string.Empty;
        hostButton.interactable = false;
    }

    public void SelectMode(string mode)
    {
        var gameMode = GameData.ParseGameMode(mode);

        List<GameData.Maps> maps = GameData.AvailableMaps(gameMode);

        map01.interactable = maps.Contains(GameData.Maps.Map01);
        winterCastle.interactable = maps.Contains(GameData.Maps.WinterCastle);
        bridgesOfFate.interactable = maps.Contains(GameData.Maps.Bridges);

        nm.gameMode = gameMode;
    }

    public void SelectMap (string mapName)
    {
        var map = GameData.ParseMap(mapName);

        nm.map = map;

        display.text = GameData.GetMapSceneName(map, nm.gameMode);

        hostButton.interactable = true;
    }

    public void OnHost()
    {

    }

    public void ModeSelect(int m)
    {

    }

    public void MapSelect(int m)
    {

    }

    public void OnCancel()
    {

    }
}
