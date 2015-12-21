using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BombController : Bolt.EntityBehaviour<IOwnedEntity> {

    public bool useAnimator;

	Animator anim;
	float lifeTimer;

	public void Awake() {
        if(useAnimator)
		    anim = GetComponent<Animator> ();
	}

	public override void Attached () {
        state.SetTransforms(state.Transform, transform);
	}

	void FixedUpdate() {
        if (!entity.isAttached)
            return;

        lifeTimer += Time.fixedDeltaTime;

        if (entity.isOwner)
        {

            if (lifeTimer > KennethData.data.bombLifeTime && entity.isOwner)
            {
                Explode();
            }
        }

        if (!useAnimator)
            return;

		if (state.Owner == null) {
			return;
		} else if (state.Owner.Team() != Player.localTeam) {
			anim.SetBool("Friendly", false);
		} else if (state.Owner.Team() == Player.localTeam) {
			anim.SetBool("Friendly", true);
		}
	}

    void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, KennethData.data.bombRadius);

        foreach (var item in hits)
        {
            if(item.tag == "Player")
            {
                if(item.GetComponentInParent<BoltEntity>().Team() != state.Owner.Team())
                {
                    item.GetComponentInParent<BoltEntity>().Damage(state.Owner, KennethData.data.bombDamage);
                }
            }
        }

        BoltNetwork.Destroy(gameObject);
    }

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player" && entity.isOwner) {
			if(other.GetComponentInParent<BoltEntity>().Team() != state.Owner.Team()) {
				Explode();
			}
		}
	}
}
