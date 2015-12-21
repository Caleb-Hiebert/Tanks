using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RadarDot : MonoBehaviour {

    public static List<Transform> trackedTransforms = new List<Transform>();

    public Transform tank;
    public RectTransform rt;
    public float lifeTime;
    public Gradient colorOverLife;
    public Color enemyColor, friendlyColor;
    public Image img;

    BoltEntity be;

    float lifeTimer = 0;
	
	void Update () {
        if (!BoltNetwork.isConnected || !be.isAttached)
        {
            Destroy(gameObject);
            return;
        }

        lifeTimer += Time.deltaTime;

        bool friendly = be.Team() == Player.localTeam;

        if ((lifeTimer > lifeTime || tank == null) && !friendly)
        {
            Destroy(gameObject);
        }

        img.color = friendly ? Color.green : Color.red;

        if (!friendly)
        {
            var a = colorOverLife.Evaluate(lifeTimer / lifeTime).a;
            var c = img.color;
            img.color = new Color(c.r, c.g, c.b, a);

            if(be.GetState<IPlayer>().Invisible)
            {
                Destroy(gameObject);
            }
        } else
        {
            var lPos = tank.position - Player.LocalPlayer.transform.position;

            lPos.x = lPos.x.Remap(0, Radar.r.visionDistance, 0, Radar.r.radarSize);
            lPos.y = lPos.y.Remap(0, Radar.r.visionDistance, 0, Radar.r.radarSize);

            rt.anchoredPosition = lPos;
        }
    }

    void Start()
    {
        trackedTransforms.Add(tank);
        be = tank.GetComponent<BoltEntity>();

        var lPos = tank.position - Player.LocalPlayer.transform.position;

        lPos.x = lPos.x.Remap(0, Radar.r.visionDistance, 0, Radar.r.radarSize);
        lPos.y = lPos.y.Remap(0, Radar.r.visionDistance, 0, Radar.r.radarSize);

        rt.anchoredPosition = lPos;
    }

    void OnDestroy()
    {
        if (tank != null)
        {
            trackedTransforms.Remove(tank);
        }
    }
}
