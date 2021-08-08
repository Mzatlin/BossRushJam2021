using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBoardOnDialogueEnd : MonoBehaviour
{
    [SerializeField] Canvas board;
    IDialogueEnd dialogue;
    [SerializeField] PlayerStatsSO stats;
    [SerializeField] Texture2D cursorTex;
    // Start is called before the first frame update
    void Start()
    {
        dialogue = GetComponent<IDialogueEnd>();
        if (board != null)
        {
            board.enabled = false;
        }
        if (dialogue != null)
        {
            dialogue.OnDialogueEnd += HandleDialogueEnd;
        }
    }

    private void OnDestroy()
    {
        if (dialogue != null)
        {
            dialogue.OnDialogueEnd -= HandleDialogueEnd;
        }
    }

    private void HandleDialogueEnd()
    {
        if (board != null && stats != null)
        {
            Cursor.SetCursor(cursorTex, Vector2.zero, CursorMode.Auto);
            board.enabled = true;
            StartCoroutine(StopDelay());
          
        }
    }
    IEnumerator StopDelay()
    {
        yield return new WaitForSeconds(0.2f);
        stats.isReady = false;
    }

}
