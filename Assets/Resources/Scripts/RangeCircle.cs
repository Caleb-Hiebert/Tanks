using UnityEngine;
using System.Collections;

public class RangeCircle : Bolt.EntityBehaviour<IPlayer> {

	public float range;
	public int circlePoints;

	private LineRenderer circle;
	private Transform point;
	
	void Start () {

		if (!entity.isOwner) {
			Destroy(GetComponent<LineRenderer>());
			Destroy(this);
		}

		circle = gameObject.GetComponent<LineRenderer>();
		
		circle.SetVertexCount (circlePoints + 1);
		circle.useWorldSpace = false;
		CreatePoints ();
	}

	void CreatePoints ()
	{
		float x;
		float y;
		float z = -9.0f;
		
		float angle = 20f;
		
		for (int i = 0; i < (circlePoints + 1); i++)
		{
			x = Mathf.Sin (Mathf.Deg2Rad * angle) * range;
			y = Mathf.Cos (Mathf.Deg2Rad * angle) * range;
			
			circle.SetPosition (i,new Vector3(x,y,z) );
			
			angle += (360f / circlePoints);
		}
	}
}
