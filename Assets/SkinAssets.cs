using UnityEngine;
using System.Collections;

public class SkinAssets : MonoBehaviour {
    public GameObject[] gameObjects;
    public AudioClip[] sounds;
    public Sprite[] sprites;
    public Texture2D[] textures;
    public Object[] genericObjects;

    public GameObject GetGameObject(string key)
    {
        foreach (var item in gameObjects)
        {
            if(item.name.Contains(key))
            {
                return item;
            }
        }

        return null;
    }

    public AudioClip GetSound(string key)
    {
        foreach (var item in sounds)
        {
            if (item.name == key)
            {
                return item;
            }
        }

        return null;
    }

    public Texture2D GetTexture(string key)
    {
        foreach (var item in textures)
        {
            if(item.name == key)
            {
                return item;
            }
        }

        return null;
    }
}
