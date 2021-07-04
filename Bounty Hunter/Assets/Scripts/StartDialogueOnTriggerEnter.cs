using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogueOnTriggerEnter : MonoBehaviour, IDialogueActivate
{
    [SerializeField] LayerMask playermask;

    public event Action OnDialogueStart = delegate { };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((1 << collision.gameObject.layer & playermask) != 0)
        {
            OnDialogueStart();
        }
    }
}
