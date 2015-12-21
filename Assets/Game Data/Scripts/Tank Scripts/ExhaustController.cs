using UnityEngine;
using System.Collections;

public class ExhaustController : Bolt.EntityBehaviour<IPlayer> {

	private ParticleSystem pSystem;
    public Gradient color;
    public AnimationCurve particleEmissions, particleSpeed;
    public float maxRPM;

	void Start () {
		pSystem = GetComponent<ParticleSystem> ();
	}
	
	void Update () {

        if (!entity.isAttached)
            return;

		float speedModifier = state.Movement.RPM;

        pSystem.startColor = color.Evaluate(speedModifier / maxRPM);
        pSystem.startSpeed = particleSpeed.Evaluate(speedModifier / maxRPM);
        pSystem.emissionRate = particleEmissions.Evaluate(speedModifier / maxRPM);
	}
}
