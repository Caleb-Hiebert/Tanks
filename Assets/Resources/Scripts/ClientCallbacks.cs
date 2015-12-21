using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Bolt;

public class ClientCallbacks : Bolt.GlobalEventListener {

    public static LayerMask mask;
	public LayerMask bulletLayerMask;
    public GameObject gameUI;
    public GameObject cam;

    void Awake()
    {
        mask = bulletLayerMask;
        DontDestroyOnLoad(this);
    }

    public override void OnEvent(AbilityEvent evnt)
    {
        var player = PlayerRegistry.GetPlayer(evnt.RaisedBy).character;
        var code = evnt.AbilityCode;

        foreach (var item in player.GetComponentsInChildren<ExtendableAbility>())
        {
            item.OnAbility(code);
        }
    }

    public override void OnEvent(GaryShot evnt) {
        var angle = evnt.Origin.LookAt2D(evnt.Direction * 500).eulerAngles.z;
        var skinAssets = evnt.Entity.GetComponentInChildren<SkinAssets>();

        var hit = Extensions.Raycast2D(evnt.Origin, evnt.Direction, 500, bulletLayerMask);

        Destroy(Instantiate(skinAssets.GetGameObject("BulletTrail")).GetComponent<HDLine>().SetData(evnt.Origin, hit.point), 0.05f);
        Instantiate(skinAssets.GetGameObject("MuzzleFlash")).GetComponent<MuzzleFlash>().SetData(evnt.Origin, angle);

        if(hit.collider != null)
        {
            BulletHit.Create(hit.collider, hit.point, angle);
        }
    }

    public override void OnEvent(RepositionEvent evnt)
    {
        evnt.Entity.transform.position = evnt.NewPosition;
        evnt.Entity.transform.rotation = evnt.Rotation;
    }

    public override void OnEvent(DeathEvent evnt)
    {
        if(evnt.Victim.hasControl && evnt.Killer != null)
        {
            GameCamera.Get().target = evnt.Killer.transform;
        }
    }

    public override void SceneLoadLocalDone(string map)
    {
        Instantiate(cam);

        if (map != "LobbyScene")
        {
            Instantiate(gameUI);
        }

        EventSystem.current.SetSelectedGameObject(null);
    }

    public override void BoltShutdownBegin(AddCallback registerDoneCallback)
    {
        Destroy(gameObject);
        Application.LoadLevel("StartScene");
    }
}
