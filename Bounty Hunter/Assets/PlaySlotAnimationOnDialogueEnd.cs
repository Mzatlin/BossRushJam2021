using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySlotAnimationOnDialogueEnd : MonoBehaviour
{

    Animator animate;
    IDialogueEnd end => GetComponent<IDialogueEnd>();
    [SerializeField] GameObject slot;
    // Start is called before the first frame update
    void Start()
    {
        animate = slot.GetComponent<Animator>();
        if (end != null)
        {
            end.OnDialogueEnd += HandleDialogueEnd;
        }
    }

    private void HandleDialogueEnd()
    {
        if(animate != null)
        {
            animate.SetTrigger("Activate");
        }
    }

    private void OnDestroy()
    {
        if (end != null)
        {
            end.OnDialogueEnd -= HandleDialogueEnd;
        }
    }
}
