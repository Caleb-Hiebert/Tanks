using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameData {

    public enum Maps
    {
        Map01, WinterCastle, Bridges
    }

    public enum GameModes
    {
        TeamDeathmatch, CaptureTheFlag, PointCapture, LastManStanding, FreeForAll
    }

    public static string GetMapSceneName(Maps map, GameModes gameMode)
    {
        string rMap = string.Empty;

        switch(gameMode)
        {
            case GameModes.FreeForAll: rMap += "FFA_";
                break;
            case GameModes.CaptureTheFlag: rMap += "CTF_";
                break;
            case GameModes.LastManStanding: rMap += "LMS_";
                break;
            case GameModes.PointCapture: rMap += "CNQ_";
                break;
            case GameModes.TeamDeathmatch: rMap += "TDM_";
                break;
        }

        switch(map)
        {
            case Maps.Map01: rMap += "Map01";
                break;
            case Maps.WinterCastle: rMap += "WinterCastle";
                break;
            case Maps.Bridges: rMap += "Bridges";
                break;
        }

        return rMap;
    }

    public static GameModes[] AvailableModes(Maps map)
    {
        switch(map)
        {
            case Maps.Bridges: return new GameModes[] { GameModes.CaptureTheFlag };
            case Maps.Map01: return new GameModes[] { GameModes.CaptureTheFlag, GameModes.PointCapture, GameModes.TeamDeathmatch };
            case Maps.WinterCastle: return new GameModes[] { GameModes.CaptureTheFlag, GameModes.PointCapture, GameModes.TeamDeathmatch };
        }

        return null;
    }

    public static List<Maps> AvailableMaps(GameModes map)
    {
        switch (map)
        {
            case GameModes.CaptureTheFlag:
            case GameModes.FreeForAll:
            case GameModes.LastManStanding:
            case GameModes.PointCapture:
            case GameModes.TeamDeathmatch: return new List<Maps> { Maps.Map01, Maps.WinterCastle, Maps.Bridges };
        }

        return null;
    }

    public static Maps ParseMap(string name)
    {
        return (Maps)Enum.Parse(typeof(Maps), name);
    }

    public static GameModes ParseGameMode(string gameMode)
    {
        return (GameModes)Enum.Parse(typeof(GameModes), gameMode);
    }
}
