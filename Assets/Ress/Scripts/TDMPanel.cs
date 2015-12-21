using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TDMPanel : MonoBehaviour {

    public static TDMPanel tdm;

    [SerializeField] GameObject panel;
    [SerializeField] Text time, blueKills, redKills;

    void Awake()
    {
        tdm = this;

        time.text = "00:00";
        blueKills.text = redKills.text = "0";
    }

    void Update()
    {
        if(BoltNetwork.isConnected)
        {
            time.text = BoltNetwork.serverTime.FormatSeconds();
        }
    }

    public bool Active
    {
        set { panel.SetActive(value); }
    }

    public int RedKills
    {
        set { redKills.text = value.ToString(); }
    }

    public int BlueKills
    {
        set { blueKills.text = value.ToString(); }
    }
}
