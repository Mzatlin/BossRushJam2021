using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBountyBoardOnInteract : MonoBehaviour
{
    [SerializeField] Canvas board;
    [SerializeField] Texture2D cursorTex;
    IInteract interact;
    // Start is called before the first frame update
    void Start()
    {
        interact = GetComponent<IInteract>();
        if(board != null)
        {
            board.enabled = false;
        }
        if(interact != null)
        {
            interact.OnInteraction += HandleInteraction;
        }
    }

    private void OnDestroy()
    {
        if (interact != null)
        {
            interact.OnInteraction -= HandleInteraction;
        }
    }

    private void HandleInteraction()
    {
        if(board != null)
        {
            Cursor.SetCursor(cursorTex, Vector2.zero, CursorMode.Auto);
            board.enabled = true;
        }
    }
}
