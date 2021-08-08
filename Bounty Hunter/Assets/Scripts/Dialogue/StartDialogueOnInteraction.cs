using UnityEngine;
using System;

public class StartDialogueOnInteraction : MonoBehaviour, IDialogueActivate
{
    public event Action OnDialogueStart = delegate { };
    IInteract interact;
    LoadDialogueBasedOnDay nextDialogue => GetComponent<LoadDialogueBasedOnDay>();
    bool hasTalked = false;
    // Start is called before the first frame update
    void Start()
    {
        hasTalked = false;
        interact = GetComponent<IInteract>();
        if (interact != null)
        {
            interact.OnInteraction += HandleInteraction;
        }
    }

    void OnDestroy()
    {
        if (interact != null)
        {
            interact.OnInteraction -= HandleInteraction;
        }
    }

    private void HandleInteraction()
    {
        if (nextDialogue != null && !hasTalked)
        {
            nextDialogue.SetNextDialogue();
            hasTalked = true;
        }
        OnDialogueStart();
    }



}
