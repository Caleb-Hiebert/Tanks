using UnityEngine;
using System.Collections;

public class TerrainSampler : MonoBehaviour {

    public string terrain;
    public TerrainType.TerrainData advancedTerrain;
    public Transform terrainPoint;
    public LayerMask terrainMask = new LayerMask();
    public float terrainSamplesPerSecond = 10;

    float lastSample = 0;

    void Awake()
    {
        terrain = "The Nothingness of the Void";
    }

    void Update()
    {
        if(Time.time > lastSample + 1 / terrainSamplesPerSecond && terrainPoint != null)
        {
            advancedTerrain = getTerrain(terrainPoint.position);
            lastSample = Time.time;
        }
    }

	TerrainType.TerrainData getTerrain(Vector3 pos) {

        var t = new TerrainType.TerrainData();
		float zPriority = 100;

		RaycastHit2D[] colliders = Physics2D.RaycastAll (new Vector2 (pos.x, pos.y), Vector2.up, 0.1f, terrainMask);

		foreach (RaycastHit2D hit in colliders) {
            var temp = hit.collider.GetComponent<TerrainType>().Data;
			float z = hit.collider.transform.position.z;

			if(z < zPriority) {
			    zPriority = z;
				t = temp;
            }
		}

		return t;
	}

    public TerrainType.TerrainData Get()
    {
        return advancedTerrain;
    }
}
