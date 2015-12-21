using UnityEngine;
using System.Collections;

public class GameDataHandler : Bolt.EntityBehaviour<IGameData> {
    public delegate void GameDataAttachedEvent(IGameData state);
    public static event GameDataAttachedEvent OnGameDataAttached;

    public static IGameData gameData;

    public override void Attached()
    {
        gameData = state;

        if(OnGameDataAttached != null)
        {
            OnGameDataAttached(state);
        }

        state.AddCallback("TDM.BlueKills", OnBlueTDMKills);
        state.AddCallback("TDM.RedKills", OnRedTDMKills);
    }

    void OnBlueTDMKills()
    {
        TDMPanel.tdm.BlueKills = state.TDM.BlueKills;
    }

    void OnRedTDMKills()
    {
        TDMPanel.tdm.RedKills = state.TDM.RedKills;
    }
}
