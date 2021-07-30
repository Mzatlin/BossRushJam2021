using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ActivateDialogueOnStart : MonoBehaviour, IDialogueActivate
{
    public event Action OnDialogueStart = delegate { };
    LoadDialogueBasedOnDay dialogue => GetComponent<LoadDialogueBasedOnDay>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay());

    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        dialogue.SetNextDialogue();
        OnDialogueStart();
    }

}
