using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueStateController : MonoBehaviour
{
    [SerializeField] PlayerStatsSO playerStats;
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
        playerStats.isReady = false;
    }
    private void HandleEnd()
    {
        playerStats.isReady = true;
    }

}
