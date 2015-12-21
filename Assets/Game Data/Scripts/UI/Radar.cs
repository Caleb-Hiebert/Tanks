using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Radar : MonoBehaviour {

    public static Radar r;

    public float visionDistance, scanSpeed, radarSize;
    public Transform localTank, dotsPanel;

    public Image radarLine;

    public GameObject dotPrefab;

    void Awake() {
        r = this;
    }

    void FixedUpdate()
    {
        if (localTank != null)
        {
            foreach (var item in PlayerRegistry.connectedEntities)
            {
                if (Vector2.Distance(item.transform.position, localTank.position) < visionDistance && !item.GetState<IPlayer>().Invisible)
                {
                    if(localTank.LookAt2D(item.transform).eulerAngles.z.Within(radarLine.transform.eulerAngles.z, 4))
                    {
                        CreateDot(item.transform);
                    }
                }
            }
        } else
        {
            if(Player.LocalPlayer != null)
            {
                localTank = Player.LocalPlayer.transform;
            }
        }

        radarLine.transform.Rotate(0, 0, scanSpeed * Time.deltaTime);
    }

    void CreateDot(Transform tank)
    {
        if (RadarDot.trackedTransforms.Contains(tank))
            return;

        var newDot = Instantiate(dotPrefab);
        newDot.transform.SetParent(dotsPanel, false);
        newDot.GetComponent<RadarDot>().tank = tank;
    }
}
