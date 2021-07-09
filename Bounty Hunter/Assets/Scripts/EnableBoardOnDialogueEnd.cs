using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBoardOnDialogueEnd : MonoBehaviour
{
    [SerializeField] Canvas board;
    IDialogueEnd dialogue;
    [SerializeField] PlayerStatsSO stats;
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
            board.enabled = true;
            stats.isReady = false;
        }
    }
}
