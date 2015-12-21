using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class LeafPlant : MonoBehaviour {

    public bool generate;

    public int minLeaves, maxLeaves;
    public float minLeafSize, maxLeafSize, minFlowerSize, maxFlowerSize;
    public Gradient randcolor;

    public Sprite[] leaves;
    public Sprite[] flowers;

	// Use this for initialization
	void Start () {
        Clear();
        Generate();
	}

    void Update()
    {
        if(generate)
        {
            generate = false;
            Start();
        }
    }

    void Clear()
    {
        foreach (var item in gameObject.GetComponentsInChildren<Transform>())
        {
            if(item != transform)
            {
                DestroyImmediate(item.gameObject);
            }
        }
    }
	
	void Generate () {
        for (int i = 0; i < Random.Range(minLeaves, maxLeaves); i++)
        {
            var g = new GameObject("Leaf");
            g.transform.SetParent(transform, false);
            g.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-360, 360));
            g.transform.localScale = Vector3.one * Random.Range(minLeafSize, maxLeafSize);
            g.AddComponent<SpriteRenderer>().sprite = leaves[Random.Range(0, leaves.Length)];
            g.GetComponent<SpriteRenderer>().color = randcolor.Evaluate(Random.Range(0f, 1f));
        }

        var f = new GameObject("Flower");
        f.transform.SetParent(transform, false);
        f.AddComponent<SpriteRenderer>().sprite = flowers[Random.Range(0, flowers.Length)];
        f.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-360, 360));
        f.transform.localScale = Vector3.one * Random.Range(minFlowerSize, maxFlowerSize);
        f.transform.localPosition = new Vector3(0, 0, -0.2f);
    }
}
