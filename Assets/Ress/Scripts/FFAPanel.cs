using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FFAPanel : MonoBehaviour {

    static FFAPanel fp;

    [SerializeField]
    Text topPlayer, gameTime;

    [SerializeField]
    GameObject panel;

    void Awake()
    {
        fp = this;
    }

	void Update ()
    {
        if (BoltNetwork.isConnected)
        {
            gameTime.text = BoltNetwork.serverTime.FormatSeconds();
        }
	}

    public static bool Enabled
    {
        set
        {
            fp.panel.SetActive(value);
        }
    }

    public static string TopPlayer
    {
        set
        {
            fp.topPlayer.text = value;
        }
    }
}
