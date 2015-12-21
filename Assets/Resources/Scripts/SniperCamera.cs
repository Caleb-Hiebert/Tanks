using UnityEngine;
using System.Collections;
using System;

public class SniperCamera : ExtendableAbility {

	public bool activeCache = false;
	public Transform sniperPoint;
    public Transform barrelOrigin;
    public LineRenderer lr;

    AbilityObject ability;

    void Update () {
        if (entity.isAttached && entity.hasControl && InputHandler.GetMouseDown(1))
        {
            SendAbility(1);
        }

        if(activeCache != state.Abilities[1].Boolean1)
        {
            activeCache = state.Abilities[1].Boolean1;
            OnChange();
        }
	}

    void LateUpdate()
    {
        if (activeCache)
        {
            var hit = Extensions.Raycast2D(barrelOrigin.position, (barrelOrigin.position - transform.position), 500f, ClientCallbacks.mask);

            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, hit.point);
        }
    }

    void OnChange()
    {
        if(state.Abilities[1].Boolean1)
        {
            Activate();
        } else
        {
            Deactivate();
        }
    }

	void Activate() {
        if (entity.isAttached && entity.hasControl)
        {
            GameCamera.Get().target = sniperPoint;

            var cursor = GetComponent<SkinAssets>().GetTexture("Cursor");
            Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.Auto);
        }

        lr.enabled = true;
	}

	public void Deactivate() {
        if (entity.isAttached && entity.hasControl)
        {
            GameCamera.Get().target = transform.parent;
            CursorSetter.ResetCursor();
        }

        lr.enabled = false;
	}

    public override void OnAbility(int code)
    {
        if(code == 1)
        {

        }
    }

    void OnDestroy()
    {
        Deactivate();
    }

    public override void OnEntityAbility(int code)
    {

    }
}
