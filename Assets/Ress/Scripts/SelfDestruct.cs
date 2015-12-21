using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

	public float delay;
	
	void Awake () {
        if (BoltNetwork.isServer || GetComponent<BoltEntity>() == null)
        {
            Destroy(this.gameObject, delay);
        } else if(GetComponent<BoltEntity>() != null && BoltNetwork.isServer)
        {
            Invoke("NetworkDestroy", delay);
        }
	}

    void NetworkDestroy()
    {
        BoltNetwork.Destroy(gameObject);
    }
}
