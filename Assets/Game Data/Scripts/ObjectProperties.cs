using UnityEngine;
using System.Collections;

public class ObjectProperties : MonoBehaviour {

    public Property material;
    public bool colorFromSprite;

    [SerializeField]
    private Color color = Color.white;

    public enum Property
    {
        SandStone, Stone, Wood, Metal, Explosive,
        MossyRock, Void
    }

    public Color Color {
        get
        {
            if(colorFromSprite && GetComponent<SpriteRenderer>() != null)
            {
                return GetComponent<SpriteRenderer>().color;
            } else
            {
                return color;
            }
        }    
    }
}
