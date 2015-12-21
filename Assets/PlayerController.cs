using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : Bolt.EntityEventListener<IPlayer> {

    public GameObject appliedState;

    public ExtendableUI tankUI;

    public override void Attached()
    {
        if(BoltNetwork.isServer)
        {
            if (Application.loadedLevelName != "LobbyScene")
            {
                state.State.ChosenState = 2;
                state.State.ChosenSkin = 1;
                state.Powerups.DamageModifier = 1;
                state.Powerups.ArmorModifier = 1;

                state.State.State = 0;
                state.State.Skin = 0;
            } else
            {
                state.State.ChosenSkin = 0;
                state.State.ChosenState = 0;
                state.State.State = 0;
                state.State.Skin = 0;
            }

            if (state.Name == null || state.Name == "")
            {
                state.Name = Utils.GetRandomName();
            }
        }

        state.SetTransforms(state.Transform, transform);
        state.AddCallback("State.State", ApplyState);
        state.AddCallback("State.Skin", ApplyState);
        state.AddCallback("Stats.Kills", OnKills);
        state.AddCallback("Stats.Assists", OnAssists);
        state.AddCallback("Stats.Deaths", OnDeaths);
        state.AddCallback("Team", OnTeam);

        OnTimer();
    }

    void Start()
    {
        PlayerRegistry.connectedPlayers.Add(state);
        PlayerRegistry.connectedEntities.Add(entity);

        OnKills();
        OnDeaths();
        OnAssists();
    }

    void ApplyState()
    {
        int playerState = state.State.State;
        int hpResetAmount = 100;
        float moveSpeed = 0, turnSpeed = 0, boostSpeed = 0;

        if (playerState == States.NoTank)
        {
            DestroyImmediate(tankUI);
            DestroyImmediate(appliedState);
        } else if (playerState == States.DeadTank)
        {

            var skinAssets = GetComponentInChildren<SkinAssets>();
            
            if(skinAssets != null)
            {
                var d = skinAssets.GetGameObject("DeathAnimation");

                if (d != null)
                {
                    Instantiate(d, transform.position, transform.rotation);
                }
            }

            skinAssets = null;
            skinAssets = GetComponentInChildren<SkinAssets>();

            if(skinAssets != null)
            {
                var deadTank = skinAssets.GetGameObject("DeadTank");

                if (deadTank != null)
                {
                    InstantiateState(deadTank);
                } else
                {
                    InstantiateState(GeneralData.data.gameObjects.Get("DeadTank"));
                }
            } else 
            {
                InstantiateState(GeneralData.data.gameObjects.Get("DeadTank"));
            }

        } else if(playerState == States.Gary)
        {
            EnforceUI<GaryUI>();

            hpResetAmount = GaryData.data.maxHealth;

            moveSpeed = GaryData.data.speed;
            turnSpeed = GaryData.data.turnSpeed;
            boostSpeed = GaryData.data.boostSpeed;

            switch (state.State.Skin)
            {
                case 0:  InstantiateState(GaryData.data.defaultSkin);
                    break;
                case 1: InstantiateState(GaryData.data.frostGary);
                    break;
                default: InstantiateState(GaryData.data.defaultSkin);
                    break;
            }
        } else if (playerState == States.Hammerhead)
        {
            EnforceUI<HammerheadUI>();

            hpResetAmount = HammerheadData.data.maxHealth;

            moveSpeed = GaryData.data.speed;
            turnSpeed = GaryData.data.turnSpeed;
            boostSpeed = GaryData.data.boostSpeed;

            switch (state.State.Skin)
            {
                case 0: InstantiateState(HammerheadData.data.defaultSkin);
                    break;
                case 1: InstantiateState(HammerheadData.data.minerHammerhead);
                    break;
                default: InstantiateState(HammerheadData.data.defaultSkin);
                    break;
            }
        } else if (playerState == States.Kenneth)
        {
            EnforceUI<KennethUI>();

            hpResetAmount = GaryData.data.maxHealth;

            moveSpeed = GaryData.data.speed;
            turnSpeed = GaryData.data.turnSpeed;
            boostSpeed = GaryData.data.boostSpeed;

            switch (state.State.Skin)
            {
                case 0: InstantiateState(KennethData.data.defaultSkin);
                    break;
                case 1: InstantiateState(KennethData.data.redDragonKenneth);
                    break;
                default: InstantiateState(KennethData.data.defaultSkin);
                    break;
            }
        } else if (playerState == States.Raven)
        {
            EnforceUI<RavenUI>();
            hpResetAmount = RavenData.data.maxHealth;

            moveSpeed = GaryData.data.speed;
            turnSpeed = GaryData.data.turnSpeed;
            boostSpeed = GaryData.data.boostSpeed;

            switch (state.State.Skin)
            {
                case 0: InstantiateState(RavenData.data.defaultSkin);
                    break;
                case 1: InstantiateState(RavenData.data.classyRaven);
                    break;
                default: InstantiateState(RavenData.data.defaultSkin);
                    break;
            }
        }

        if(playerState > 1)
        {
            GetComponent<TankInfoDisplay>().Show();

            if (entity.isOwner)
            {
                GetComponent<TankHealth>().ResetHP(hpResetAmount);
            }

            if (entity.hasControl)
            {
                GameCamera.Get().target = transform;
            }
        }

        if(entity.isOwner)
        {
            state.Movement.Speed = (int)moveSpeed;
            state.Movement.Turnspeed = (int)turnSpeed;
            state.Movement.BoostSpeed = (int)boostSpeed;
        }
    }

    void EnforceUI<T> () where T : ExtendableUI
    {
        if (tankUI != null && tankUI.GetType() != typeof(T))
        {
            DestroyImmediate(tankUI);
        }

        if (tankUI == null)
            tankUI = gameObject.AddComponent<T>();
    }

    void Update()
    {
        if (BoltNetwork.isServer)
        {
            foreach (var item in state.Abilities)
            {
                if (item.Cooldown > 0)
                {
                    item.Cooldown = Mathf.Clamp(item.Cooldown - Time.deltaTime, 0, Mathf.Infinity);
                }
            }
        }
    }

    public override void ControlGained()
    {
        if (Application.loadedLevelName != "LobbyScene")
        {
            GameCamera.Get().target = transform;
        }

        if (!BoltNetwork.isServer)
        {
            state.SetTransforms(state.Transform, null);
        }

        state.AddCallback("State.RespawnTime", OnTimer);

        Player.localTeam = state.Team;
        Player.LocalPlayer = entity;
    }

    public override void ControlLost()
    {
        state.SetTransforms(state.Transform, transform);

        state.RemoveCallback("State.RespawnTime", OnTimer);

        Player.localTeam = -1;
    }

    void InstantiateState(GameObject stateObject)
    {
        if (appliedState != null)
        {
            DestroyImmediate(appliedState);
        }

        if (stateObject != null) {

            appliedState = Instantiate(stateObject);

            appliedState.transform.SetParent(transform, false);
        }
        
    }

    public void Kill()
    {
        if (state.State.RespawnTime > 0)
            return;

        state.State.State = States.DeadTank;

        var deathEvent = DeathEvent.Create(Bolt.GlobalTargets.Everyone, Bolt.ReliabilityModes.ReliableOrdered);
        deathEvent.Victim = entity;

        var killer = GetComponent<TankHealth>().LastToDamage;

        if (killer != null)
        {
            deathEvent.Killer = killer;
            state.Stats.Deaths++;

            if (killer.StateIs<IPlayer>())
            {
                killer.GetState<IPlayer>().Stats.Kills++;
            }
        }

        deathEvent.Send();

        foreach (var item in GetComponent<TankHealth>().Assistors)
        {
            item.GetState<IPlayer>().Stats.Assists++;
        }
    }

    public void OnKills()
    {
        if(entity.isOwner && state.Stats.Kills > 0)
        {
            state.Stats.Points += (int)Stats.Points.Kill;
            if (state.Team == 1)
            {
                GameDataHandler.gameData.TDM.RedKills++;
            }
            else
            {
                GameDataHandler.gameData.TDM.BlueKills++;
            }
        }

        if(entity.hasControl)
        {
            UserInfoPanel.KillDisplay = state.Stats.Kills.ToString();
        }
    }

    public void OnAssists()
    {
        if(entity.isOwner && state.Stats.Kills > 0)
        {
            state.Stats.Points += (int)Stats.Points.Assist;
        }

        if(entity.hasControl)
        {
            UserInfoPanel.AssistDisplay = state.Stats.Assists.ToString();
        }
    }

    public void OnDeaths()
    {
        if (entity.hasControl)
        {
            UserInfoPanel.DeathDisplay = state.Stats.Deaths.ToString();
        }
    }

    public void Spawn()
    {
        if (entity.isOwner)
        {
            state.State.State = state.State.ChosenState;
            state.State.Skin = state.State.ChosenSkin;

            var point = Spawner.GetPoint(state.Team);

            var repositionEvent = RepositionEvent.Create(Bolt.GlobalTargets.Everyone);
            repositionEvent.Entity = entity;
            repositionEvent.NewPosition = point.position;
            repositionEvent.Rotation = point.rotation;
            repositionEvent.Send();
        }
    }

    IEnumerator Respawn (float time)
    {
        state.State.RespawnTime = time;

        while(state.State.RespawnTime > 0)
        {
            state.State.RespawnTime = Mathf.Clamp(state.State.RespawnTime - Time.deltaTime, 0, Mathf.Infinity);
            yield return null;
        }

        state.State.RespawnTime = 0;
        Spawn();
        yield return null;
    }

    public void RespawnDelayed(float t)
    {
        if(BoltNetwork.isServer) { 
            StartCoroutine(Respawn(t));
        }
    }

    void OnTimer()
    {
        if (SpawnMenu.timer != null)
        {
            SpawnMenu.timer.OnTimer(state.State.RespawnTime);
        }
    }

    void OnTeam()
    {
        if(entity.hasControl)
        {
            Player.localTeam = state.Team;
        }
    }

    public override void OnEvent(EntityEvent evnt)
    {
        foreach (var item in GetComponentsInChildren<ExtendableAbility>())
        {
            item.OnEntityAbility(evnt.Code);
        }
    }

    public override void Detached()
    {
        PlayerRegistry.connectedPlayers.Remove(state);
        PlayerRegistry.connectedEntities.Remove(entity);
    }
}

public static class States
{
    public static readonly int 
        NoTank = 0, 
        DeadTank = 1,
        Gary = 2,
        Hammerhead = 3,
        Kenneth = 4,
        Raven = 5;
}
