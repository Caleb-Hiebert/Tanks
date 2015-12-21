using UnityEngine;
using System.Collections;

public class GameModeCallbacks : Bolt.GlobalEventListener {

}

[BoltGlobalBehaviour("FFA_Map01", "FFA_WinterCastle", "FFA_Bridges")]
public class FreeForAllCallbacks : Bolt.GlobalEventListener
{
    int currentTeamid = 1;
    float respawnTime = 4f;

    public override void SceneLoadLocalDone(string map)
    {
        if(BoltNetwork.isServer)
        {
            var player = PlayerRegistry.serverPlayer.character;

            player.GetState<IPlayer>().Team = currentTeamid;
            currentTeamid++;

            player.GetComponent<PlayerController>().Spawn();
        }

        FFAPanel.Enabled = true;
    }

    public override void SceneLoadRemoteDone(BoltConnection connection)
    {
        if (!BoltNetwork.isServer)
            return;

        var player = PlayerRegistry.GetPlayer(connection).character;

        player.GetState<IPlayer>().Team = currentTeamid;
        currentTeamid++;

        player.GetComponent<PlayerController>().Spawn();
    }

    public override void OnEvent(DeathEvent evnt)
    {
        FFAPanel.TopPlayer = TopPlayerName();

        evnt.Victim.GetComponent<PlayerController>().RespawnDelayed(respawnTime);
    }

    public string TopPlayerName()
    {
        string nameOfPlayer = PlayerRegistry.connectedPlayers[0].Name;
        int highestPoints = 0;

        foreach (var item in PlayerRegistry.connectedPlayers)
        {
            if(item.Stats.Points > highestPoints)
            {
                highestPoints = item.Stats.Points;
                nameOfPlayer = item.Name;
            }
        }

        return nameOfPlayer;
    }
}

[BoltGlobalBehaviour("TDM_Map01", "TDM_WinterCastle", "TDM_Bridges")]
public class TeamDeathMatchCallbacks : Bolt.GlobalEventListener
{
    float respawnTime = 3.5f;
    bool teamAssignment = false;

    TDMObject data;

    struct TeamKills {
        public int red, blue;
    }

    void Start()
    {
        if(GameDataHandler.gameData == null)
        {
            GameDataHandler.OnGameDataAttached += (d) => { data = d.TDM; };
        } else
        {
            data = GameDataHandler.gameData.TDM;
        }
    }

    public override void SceneLoadRemoteDone(BoltConnection connection)
    {
        if (!BoltNetwork.isServer)
            return;

        var player = PlayerRegistry.GetPlayer(connection).character;

        AssignTeam(player);

        player.GetComponent<PlayerController>().Spawn();
    }

    public override void SceneLoadLocalDone(string map)
    {
        TDMPanel.tdm.Active = true;

        if (!BoltNetwork.isServer)
            return;

        AssignTeam(PlayerRegistry.serverPlayer.character);

        PlayerRegistry.serverPlayer.character.GetComponent<PlayerController>().Spawn();
    }

    public override void OnEvent(DeathEvent evnt)
    {
        evnt.Victim.GetComponent<PlayerController>().RespawnDelayed(respawnTime);

        if (BoltNetwork.isServer)
        {
            var kills = GetTeamKills();

            data.BlueKills = kills.blue;
            data.RedKills = kills.red;
        }
    }

    TeamKills GetTeamKills()
    {
        var s = new TeamKills();
        s.red = s.blue = 0;

        foreach (var item in PlayerRegistry.connectedPlayers)
        {
            if(item.Team == 1)
            {
                s.red += item.Stats.Kills;
            } else if (item.Team == 2)
            {
                s.blue += item.Stats.Kills;
            }
        }

        return s;
    }

    void AssignTeam(BoltEntity entity)
    {
        if (!BoltNetwork.isServer)
            return;

        if (teamAssignment)
        {
            entity.GetState<IPlayer>().Team = 1;
        }
        else
        {
            entity.GetState<IPlayer>().Team = 2;
        }

        teamAssignment = !teamAssignment;
    }
}
