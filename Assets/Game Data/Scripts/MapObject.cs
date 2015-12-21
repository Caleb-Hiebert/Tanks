using UnityEngine;
using System.Collections;

public class MapObject : MonoBehaviour {

    [SerializeField]
    GameObject fullPrefab;

    void Start()
    {
        if(BoltNetwork.isServer)
        {
            BoltNetwork.Instantiate(fullPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
