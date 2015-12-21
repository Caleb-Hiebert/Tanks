using UnityEngine;
using System.Collections;

public class HookController : Bolt.EntityBehaviour<IGrappleState> {

    public BoltEntity owner;
    public Transform originPoint;

    private Vector2 localSpaceHit;
    private BoltEntity entityHit;

    bool hitEntity;

    float rotationOffset;

	[SerializeField] LineRenderer rope;

	public override void Attached() {

        var token = (GrappleToken)entity.attachToken;

        originPoint = token.owner.GetComponentInChildren<PointOfInterest>().transform;
        transform.rotation = originPoint.rotation;

        entityHit = token.entityHit;

        hitEntity = entityHit != null;

        owner = token.owner;

        try
        {
            owner.GetComponentInChildren<HammerheadGrapple>().activeHook = gameObject;
        } catch { }

        if(!hitEntity)
        {
            transform.position = token.worldHitPoint;
        } else
        {
            localSpaceHit = token.entityLocalPosition;
            rotationOffset = entityHit.transform.eulerAngles.z - transform.eulerAngles.z;
        }
	}
	
	void Update () {

        if (owner == null && entity.isOwner)
        {
            BoltNetwork.Destroy(gameObject);
        }

        if(hitEntity)
        {
            transform.position = entityHit.transform.TransformPoint(localSpaceHit);
            transform.rotation = Quaternion.Euler(0, 0, entityHit.transform.eulerAngles.z + rotationOffset * -1);
        }

        rope.SetPosition(0, originPoint.position.Z(-2));
        rope.SetPosition(1, transform.position.Z(-2));
	}
}
