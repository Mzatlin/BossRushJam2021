using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySlotAnimationOnDialogueEnd : MonoBehaviour
{

    Animator animate, storyboardAnimate;
    IDialogueEnd end => GetComponent<IDialogueEnd>();
    [SerializeField] GameObject slot;
    [SerializeField] GameObject storyboard;
    // Start is called before the first frame update
    void Start()
    {
        animate = slot.GetComponent<Animator>();
        storyboardAnimate = storyboard.GetComponent<Animator>();
        if (end != null)
        {
            end.OnDialogueEnd += HandleDialogueEnd;
        }
    }

    private void HandleDialogueEnd()
    {
        if (storyboardAnimate != null)
        {
            storyboardAnimate.SetTrigger("Activate");
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        if (animate != null)
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
