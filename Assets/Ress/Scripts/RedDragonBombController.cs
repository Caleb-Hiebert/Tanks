using UnityEngine;
using System.Collections;

public class RedDragonBombController : MonoBehaviour {

    public ParticleSystem particles;

    IOwnedEntity entity;

    void Start()
    {
        entity = GetComponent<BoltEntity>().GetState<IOwnedEntity>();
    }
	
	void Update () {
        bool friendly = entity.Owner.Team() == Player.localTeam;

        if(friendly)
        {
            particles.startColor = Color.green;
        } else
        {
            particles.startColor = Color.red;
        }
	}
}
