using UnityEngine;
using System.Collections;

public class SpeedPowerupParticles : Bolt.EntityBehaviour<IPlayer> {

    public ParticleSystem system;
    public float emissionsRate;
	
	void Update () {
        if (!entity.isAttached)
            return;

        system.emissionRate = state.Powerups.SpeedPowerup > 0 ? emissionsRate : 0;
	}
}
