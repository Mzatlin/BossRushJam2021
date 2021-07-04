using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueStateController : MonoBehaviour
{
    [SerializeField] PlayerStatsSO playerStats;
    IDialogueActivate activate;
    // Start is called before the first frame update
    void Start()
    {
        activate = GetComponent<IDialogueActivate>();
        if(activate != null)
        {
            activate.OnDialogueStart += HandleActivation;
        }
    }
    void OnDestroy()
    {
        if(activate != null)
        {
            activate.OnDialogueStart -= HandleActivation;
        }    
    }
    private void HandleActivation()
    {
        playerStats.isReady = false;
    }
}
