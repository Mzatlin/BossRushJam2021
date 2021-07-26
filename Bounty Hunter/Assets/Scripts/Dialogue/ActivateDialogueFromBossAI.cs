using System;
using UnityEngine;

public class ActivateDialogueFromBossAI : MonoBehaviour, IDialogueActivate
{
    public event Action OnDialogueStart = delegate { };

    public void ActivateDialogue()
    {
        OnDialogueStart();
    }
}
