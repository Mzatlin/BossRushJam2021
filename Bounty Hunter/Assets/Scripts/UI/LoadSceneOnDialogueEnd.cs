using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnDialogueEnd : MonoBehaviour
{
    IDialogueEnd dialogueEnd;
    // Start is called before the first frame update
    void Start()
    {
        dialogueEnd = GetComponent<IDialogueEnd>();
        if(dialogueEnd != null)
        {
            dialogueEnd.OnDialogueEnd += HandleDialogueEnd;
        }
    }

    private void OnDestroy()
    {
        if(dialogueEnd != null)
        {
            dialogueEnd.OnDialogueEnd -= HandleDialogueEnd;
        }
    }

    private void HandleDialogueEnd()
    {
        SceneManager.LoadScene(2);
    }

}
