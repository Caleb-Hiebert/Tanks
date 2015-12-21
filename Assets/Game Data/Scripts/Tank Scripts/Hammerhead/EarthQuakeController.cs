using UnityEngine;
using ParticlePlayground;
using System.Collections;

public class EarthQuakeController : Bolt.EntityBehaviour<IOwnedEntity> {

    public ParticleSystem explosionSystem;

    public PlaygroundParticlesC windupSystem;

    public SpriteRenderer cracks;

    void Start()
    {
        Invoke("Boom", HammerheadData.data.earthquakeWindupTime);
        Invoke("Remove", 20);
    }

    void Boom()
    {
        windupSystem.emissionRate = 0;
        windupSystem.manipulators[1].enabled = true;
        windupSystem.manipulators[0].enabled = false;
        explosionSystem.Play();

        foreach (var item in transform.position.GetPlayersInRadius(HammerheadData.data.earthquakeRadius))
        {
            if (item.Team() != state.Owner.Team())
            {
                item.Damage(state.Owner, HammerheadData.data.earthquakeDamage);
            }
        }

        cracks.enabled = true;
    }
    
    void Remove()
    {
        if(BoltNetwork.isServer)
        {
            BoltNetwork.Destroy(gameObject);
        }
    }
}
