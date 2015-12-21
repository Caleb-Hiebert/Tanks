using UnityEngine;
using System;

public class BulletHit : MonoBehaviour {

    static BulletHit h;

    public GameObject[] bulletHits;

    public enum RotationMode
    {
        Normal, Reverse, None, Both
    }

    void Awake()
    {
        h = this;
    }

    public static void Create(Collider2D collider, Vector2 location, float rotation)
    {
        var material = GetMaterial(collider);

        if (material == ObjectProperties.Property.Void)
            return;

        float rotationOffset = 0;

        var rotationMode = h.GetRotationMode(material);

        if (rotationMode == RotationMode.Reverse)
        {
            h.SpawnBulletHit(material, location, rotation, 180);
        } else if(rotationMode == RotationMode.None)
        {
            h.SpawnBulletHit(material, location);
        } else if (rotationMode == RotationMode.Normal)
        {
            h.SpawnBulletHit(material, location, rotation, 0);
        } else if (rotationMode == RotationMode.Both)
        {
            h.SpawnBulletHit(material, location, rotation, 0);
            h.SpawnBulletHit(material, location, rotation, 180);
        }
    }

    void SpawnBulletHit(ObjectProperties.Property material, Vector2 location, float rotation, float rotationOffset)
    {
        var rot = Quaternion.Euler(0, 0, rotation + rotationOffset);
        var prefab = GetPrefab(material);

        Instantiate(prefab, location, rot);
    }

    void SpawnBulletHit(ObjectProperties.Property material, Vector2 location)
    {
        var prefab = GetPrefab(material);

        Instantiate(prefab, location, Quaternion.identity);
    }

    static ObjectProperties.Property GetMaterial(Collider2D collider)
    {
        var props = collider.GetComponent<ObjectProperties>();

        if(props != null)
        {
            return props.material;
        } else
        {
            return ObjectProperties.Property.Metal;
        }
    }

    RotationMode GetRotationMode(ObjectProperties.Property material)
    {
        switch(material)
        {
            case ObjectProperties.Property.Metal: return RotationMode.Normal;
            case ObjectProperties.Property.SandStone: return RotationMode.None;
            case ObjectProperties.Property.Stone: return RotationMode.Reverse;
            case ObjectProperties.Property.Wood: return RotationMode.Normal;
            case ObjectProperties.Property.MossyRock: return RotationMode.None;
            default: return RotationMode.Normal;
        }
    }

    GameObject GetPrefab(ObjectProperties.Property material)
    {
        return Get(Enum.GetName(typeof(ObjectProperties.Property), material));
    }

    GameObject Get(string name)
    {
        foreach (var item in bulletHits)
        {
            if(item.name == name)
            {
                return item;
            }
        }

        return null;
    }
}
