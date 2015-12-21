using UnityEngine;
using System.Collections;

public class RocketAI : Bolt.EntityBehaviour<IOwnedEntity> {

	public AnimationCurve speedOverLife = new AnimationCurve();

	[SerializeField] private LayerMask hitMask;

	private Vector3 spawnPos;
	private float spawnTime;
	private GameObject temporaryRocketTarget;
	private float lifeTimer;

	private float lastKeyframeTime = 0;
	private float firstKeyframeTime = 0;

	public override void Attached () {
        state.SetTransforms(state.Transform, transform);

		spawnTime = Time.time;

		if (entity.isOwner) {

			Keyframe[] keys = speedOverLife.keys;

			foreach (Keyframe k in keys) {
				if(k.time > lastKeyframeTime) lastKeyframeTime = k.time;
				else if(k.time < firstKeyframeTime) firstKeyframeTime = k.time;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (state.Owner != null) {

			if (entity.isOwner && other.gameObject.tag != "Rocket" && other.gameObject.layer == hitMask.value && other.transform.parent.gameObject != state.Owner.gameObject) {

                if(other.tag == "Player" && other.GetComponentInParent<BoltEntity>().Team() != state.Owner.Team())
                {
                    other.GetComponentInParent<BoltEntity>().Damage(state.Owner, KennethData.data.rocketDamage);
                }

                Boom();
			}
		}
	}

    void Boom()
    {
        BoltNetwork.Destroy(gameObject);
    }

    public override void Detached()
    {
        Instantiate(state.Owner.GetComponentInChildren<SkinAssets>().GetGameObject("RocketDeath"), transform.position, Quaternion.identity);
    }

	void FixedUpdate () {

        if (!entity.isOwner)
            return;

		lifeTimer += Time.fixedDeltaTime;

        var target = state.Owner.GetState<IPlayer>().MousePosition;

		var dir = target - transform.position;
		var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		angle -= 90;
		float ang = Mathf.LerpAngle (transform.eulerAngles.z, angle, BoltNetwork.frameDeltaTime * KennethData.data.rocketTurnSpeed);
		transform.rotation = Quaternion.AngleAxis (ang, Vector3.forward);
		transform.Translate (0, speedOverLife.Evaluate((lifeTimer / KennethData.data.rocketLifeTime) * 100) * Time.deltaTime, 0);

		if ( Time.time > spawnTime + KennethData.data.rocketLifeTime) {
            Boom();
		}
	}
}
