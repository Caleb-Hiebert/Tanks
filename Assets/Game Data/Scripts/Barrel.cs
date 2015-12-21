using UnityEngine;
using System.Collections;

public class Barrel : Bolt.EntityBehaviour<IPlayer> {

	void LateUpdate () {
        if (!entity.hasControl && !state.Stunned)
        {
            transform.rotation = transform.LookAt2D(state.MousePosition);
        } else if (entity.hasControl && !state.Stunned)
        {
            transform.rotation = transform.LookAt2D(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
	}
}
