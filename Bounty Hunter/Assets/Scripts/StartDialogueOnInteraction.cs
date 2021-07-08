using UnityEngine;
using System;

public class StartDialogueOnInteraction : MonoBehaviour, IDialogueActivate
{
    public event Action OnDialogueStart = delegate { };
    IInteract interact;
    // Start is called before the first frame update
    void Start()
    {
        interact = GetComponent<IInteract>();
        if(interact != null)
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
        OnDialogueStart();
    }



}
