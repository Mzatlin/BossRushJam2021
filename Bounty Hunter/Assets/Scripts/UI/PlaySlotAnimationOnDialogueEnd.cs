using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySlotAnimationOnDialogueEnd : MonoBehaviour
{

    Animator animate, storyboardAnimate, liftAnimation;
    IDialogueEnd end => GetComponent<IDialogueEnd>();
    [SerializeField] GameObject slot;
    [SerializeField] GameObject storyboard;
    [SerializeField] GameObject gunLift;
    [SerializeField] CurrentDaySO day;
    // Start is called before the first frame update
    void Start()
    {
        animate = slot.GetComponent<Animator>();
        storyboardAnimate = storyboard.GetComponent<Animator>();
        if (end != null)
        {
            end.OnDialogueEnd += HandleDialogueEnd;
        }
        if(gunLift != null)
        {
            liftAnimation = gunLift.GetComponent<Animator>();
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
        if (animate != null && day != null && day.currentDay < 3)
        {
            animate.SetTrigger("Activate");
        }
        else
        {
            liftAnimation.SetTrigger("Activate");
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
