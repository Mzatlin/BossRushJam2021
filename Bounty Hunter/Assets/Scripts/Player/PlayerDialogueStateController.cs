using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueStateController : MonoBehaviour
{
    [SerializeField] PlayerStatsSO playerStats;
    [SerializeField] BossDialogueSO boss;
    IDialogueActivate activate;
    IDialogueEnd end;
    // Start is called before the first frame update
    void Start()
    {
        activate = GetComponent<IDialogueActivate>();
        end = GetComponent<IDialogueEnd>();
        if(activate != null && end != null)
        {
            activate.OnDialogueStart += HandleActivation;
            end.OnDialogueEnd += HandleEnd;
        }
    }


    void OnDestroy()
    {
        if(activate != null && end != null)
        {
            activate.OnDialogueStart -= HandleActivation;
            end.OnDialogueEnd -= HandleEnd;
        }    
    }
    private void HandleActivation()
    {
        playerStats.isInDialogue = true;
    }
    private void HandleEnd()
    {
        if(boss == null || boss.isOpeningSet)
        {
            playerStats.isInDialogue = false;
            playerStats.isReady = true;
        }
    }

}
