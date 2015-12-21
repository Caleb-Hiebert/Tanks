using UnityEngine;
using System.Collections;
using System;

public class HammerheadGrapple : ExtendableAbility {

	public GameObject grappleHook;
	public Transform rayPoint;

    public bool reeling;

	public GameObject activeHook;
	[SerializeField] LayerMask grappleMask;
	public Sound sound;

    void Update()
    {
        if(entity.hasControl)
        {
            if(InputHandler.GetMouseDown(0) && state.Abilities[0].Cooldown == 0)
            {
                SendAbility(0);
                sound.PlaySound("GrappleThud");
            }
        }

        if (!state.Abilities[0].Boolean1 && state.Abilities[0].Cooldown == 0)
        {
            grappleHook.SetActive(true);
        } else
        {
            grappleHook.SetActive(false);
        }
    }

    public override void OnAbility(int code)
    {
        if (code == 0 && state.Abilities[0].Boolean1 == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, rayPoint.TransformPoint(Vector3.up) - rayPoint.position, 50, grappleMask);

            var gToken = new GrappleToken();

            if (hit.collider != null)
            {
                state.Abilities[0].Boolean1 = true;

                gToken.owner = entity;

                if (hit.collider.GetComponentInParent<BoltEntity>() != null)
                {
                    var hitEntity = hit.collider.GetComponentInParent<BoltEntity>();
                    gToken.entityHit = hitEntity;
                    gToken.entityLocalPosition = hit.collider.transform.InverseTransformPoint(hit.point);

                    if (hitEntity.Team() != state.Team)
                    {
                        hitEntity.Damage(entity, HammerheadData.data.grappleDamage);
                    }
                }
                else
                {
                    gToken.worldHitPoint = hit.point;
                }

                activeHook = BoltNetwork.Instantiate(SkinAssets.GetGameObject("Grapple"), gToken);
            } else
            {
                state.Abilities[0].Cooldown = HammerheadData.data.grappleCooldown;
            }
        }
        else if (code == 0)
        {
            SentEntityEvent(0);
        } else if (code == 10)
        {
            state.Abilities[0].Boolean1 = false;
            state.Abilities[0].Cooldown = HammerheadData.data.grappleCooldown;

            if(activeHook != null)
                BoltNetwork.Destroy(activeHook);
        }
    }

    IEnumerator Reel(Transform goTo)
    {
        reeling = true;
        float startTime = Time.time;

        while (reeling)
        {
            transform.parent.position = Vector3.Lerp(transform.parent.position, goTo.position, HammerheadData.data.reelSpeed * Time.deltaTime);

            if (Utils.WithinRange(transform.position.x, goTo.position.x, HammerheadData.data.grappleBreakDistance) && Utils.WithinRange(transform.position.y, goTo.position.y, HammerheadData.data.reelSpeed))
            {
                break;
            }
            else if (Time.time >= startTime + HammerheadData.data.reelBreakTime)
            {
                break;
            }

            yield return new WaitForFixedUpdate();
        }

        reeling = false;

        SendAbility(10);
    }

    public override void OnEntityAbility(int code)
    {
        if (code == 0 && !reeling && activeHook != null && entity.hasControl)
        {
            StartCoroutine(Reel(activeHook.transform));
        }
    }

    void OnDestroy()
    {
        if(entity.isAttached && entity.isOwner && activeHook != null)
        {
            BoltNetwork.Destroy(activeHook);
        }
    }
}
