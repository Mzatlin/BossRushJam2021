using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SetNewDialogueBase : MonoBehaviour
{

    protected List<List<string>> phaseDialogue = new List<List<string>>();
    protected int index = 0;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        InitializeListOfDialogue();
    }

    protected abstract void InitializeListOfDialogue();
}
