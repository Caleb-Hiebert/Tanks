using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnMenu : Bolt.EntityBehaviour<IPlayer> {

    public static SpawnMenu timer;
	public GameObject spawnMenu;
	public Text text;

    void Awake()
    {
        timer = this;
        spawnMenu.SetActive(false);
    }

    public void OnTimer(float t)
    {
        if (t > 0)
        {
            spawnMenu.SetActive(true);

            float time = Mathf.Round(t * 100) / 100;

            string[] stringTime = time.ToString().Split('.');

            string milli;

            if(stringTime.Length == 1)
            {
                milli = "00";
            } else if (stringTime[1].Length == 1)
            {
                milli = stringTime[1] + "0";
            } else
            {
                milli = stringTime[1];
            }

            text.text = string.Format("{0}:{1}", stringTime[0], milli);
        } else if (t == 0)
        {
            spawnMenu.SetActive(false);
        }
    }
}
