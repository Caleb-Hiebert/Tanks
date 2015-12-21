using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Tiler : MonoBehaviour {
    public bool tile;

    public int cols, rows;

    public float imgSize;

    public Sprite[] tiles;

    void Update()
    {
        if(tile)
        {
            tile = false;
            Tile();
        }
    }

    void Tile()
    {
        foreach (var item in tiles)
        {
            string[] nameSplit = item.name.Split('_');
            int c = int.Parse(nameSplit[1].Replace("C", "").Trim());
            int r = int.Parse(nameSplit[2].Replace("R", "").Trim());

            var t = new GameObject("Tile " + c + " " + r);
            t.AddComponent<SpriteRenderer>().sprite = item;
            t.transform.position = new Vector2((c - cols) * imgSize, (r - rows) * -imgSize);
        }
    }
}
