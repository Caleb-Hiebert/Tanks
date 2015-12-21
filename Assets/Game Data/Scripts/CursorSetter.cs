using UnityEngine;
using System.Collections;

public class CursorSetter : MonoBehaviour {
    static CursorSetter cs;

    public Texture2D defaultCursor;

    void Awake()
    {
        cs = this;
        ResetCursor();
    }

    public static void ResetCursor()
    {
        Cursor.SetCursor(cs.defaultCursor, new Vector2(cs.defaultCursor.width / 2, cs.defaultCursor.height / 2), CursorMode.Auto);
    }
}
