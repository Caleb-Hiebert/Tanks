using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class InputHandler {

    public static bool inputBlocked;

    static float lastChecked = 0;
    static float checkEvery = 0.05f;

    public static float GetAxis(string axis)
    {
        if (Input.GetAxis(axis) != 0)
        {
            ForceCheckInputBlocked();

            if (!inputBlocked)
                return Input.GetAxis(axis);
        }

        return 0;
    }

    public static bool GetKeyDown(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            ForceCheckInputBlocked();

            if (!inputBlocked)
                return true;
        }

        return false;
    }

    public static bool GetKey(KeyCode key)
    {
        if(Input.GetKey(key))
        {
            ForceCheckInputBlocked();

            if (!inputBlocked)
                return true;
        }

        return false;
    }

    public static bool GetMouseDown(int mouseButton)
    {
        if (Input.GetMouseButtonDown(mouseButton))
        {
            ForceCheckInputBlocked();

            if (!inputBlocked)
                return true;
        }

        return false;
    }

    public static bool GetMouse(int mouseButton)
    {
        if (Input.GetMouseButton(mouseButton))
        {
            ForceCheckInputBlocked();

            if (!inputBlocked)
                return true;
        }

        return false;
    }

    public static bool CanEscapeMenu()
    {
        if (TankSelector.enabled)
        {
            return false;
        }

        if (EventSystem.current != null)
            if (EventSystem.current.currentSelectedGameObject != null || EventSystem.current.alreadySelecting)
            {
                return false;
            }

        return true;
    }

    public static void ForceCheckInputBlocked()
    {
        lastChecked = Time.time;

        inputBlocked = false;

        if (EscapeMenu.em != null)
        {
            if (EscapeMenu.em.MenuEnabled == true)
            {
                inputBlocked = true;
            }
        }

        if (Player.LocalPlayerState != null)
        {
            if (Player.LocalPlayerState.Stunned)
            {
                inputBlocked = true;
            }
        }

        if (TankSelector.enabled)
        {
            inputBlocked = true;
        }

        if (EventSystem.current != null)
            if (EventSystem.current.currentSelectedGameObject != null || EventSystem.current.alreadySelecting)
            {
                inputBlocked = true;
            }
    }
}
