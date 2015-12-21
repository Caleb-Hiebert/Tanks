using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class TankSelector : MonoBehaviour {

    public Text tankName, tankStats, tankCustom;

    public Image[] tankBoxes;

    public static bool enabled;

    public Color selectedColor, defaultColor;

    public int state, skin;

    DataBase current;

    void Start()
    {
        Select("Gary");
    }

    public void SetData(DataBase info)
    {
        current = info;

        tankName.text = info.tankName;
        tankStats.text = string.Format("Staring Health: {0}\nMove Speed: {1}\nTurn Speed: {2}\nBoost Speed: {3}", info.maxHealth, info.speed, info.turnSpeed, info.boostSpeed);
    }

    public void Select(string name)
    {
        var t = TankData.Parse(name);

        SetData(TankData.GetTankData(t));
        var st = TankData.GetState(t);

        state = st.state;
        skin = st.skin;
    }

    public void Test(GameObject go)
    {
        foreach (var item in tankBoxes)
        {
            item.color = defaultColor;
        }

        go.GetComponent<Image>().color = selectedColor;
    }

    public void Select()
    {
        var evnt = StateChangeEvent.Create(Bolt.GlobalTargets.OnlyServer);
        evnt.State = state;
        evnt.Skin = skin;
        evnt.Send();

        enabled = false;
        gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    void OnEnable()
    {
        enabled = true;
    }
}
