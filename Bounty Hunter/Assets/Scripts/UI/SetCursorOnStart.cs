using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursorOnStart : MonoBehaviour
{
    [SerializeField] Texture2D cursorTex;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.SetCursor(cursorTex, Vector2.zero, CursorMode.Auto);
    }

}
