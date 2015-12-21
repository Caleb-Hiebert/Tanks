using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[BoltGlobalBehaviour(BoltNetworkModes.Host)]
public class Spawner : Bolt.GlobalEventListener {

    public static bool ready;

    public static List<Transform> blueSpawnPoints, redSpawnPoints;

    void Awake()
    {
        blueSpawnPoints = new List<Transform>();
        redSpawnPoints = new List<Transform>();
    }

    public override void SceneLoadLocalBegin(string map)
    {
        ready = false;
    }

    static void GetPoints()
    {
        if(blueSpawnPoints.Count != 0)
            blueSpawnPoints.Clear();

        if (redSpawnPoints.Count != 0)
            redSpawnPoints.Clear();

        foreach (var item in GameObject.FindGameObjectsWithTag("SpawnPoint_B"))
        {
            blueSpawnPoints.Add(item.transform);
        }

        foreach (var item in GameObject.FindGameObjectsWithTag("SpawnPoint_R"))
        {
            redSpawnPoints.Add(item.transform);
        }

        ready = true;
    }

    public struct Point
    {
        public Vector2 position;
        public Quaternion rotation;

        public Point(Transform arg)
        {
            position = arg.position;
            rotation = arg.rotation;
        }
    }

    public static Point GetPoint(int team)
    {
        if (!ready)
            GetPoints();

        Point p;

        if(team == 1)
        {
            p = new Point(redSpawnPoints[Random.Range(0, redSpawnPoints.Count - 1)]);
        } else if (team == 2)
        {
            p = new Point(blueSpawnPoints[Random.Range(0, blueSpawnPoints.Count - 1)]);
        } else
        {
            if(Random.Range(0, 1) > 0.5f)
            {
                p = new Point(redSpawnPoints[Random.Range(0, redSpawnPoints.Count - 1)]);
            } else
            {
                p = new Point(blueSpawnPoints[Random.Range(0, blueSpawnPoints.Count - 1)]);
            }
        }

        return p;
    }
}
