using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerOnInteraction : MonoBehaviour
{
    [SerializeField] PlayerStatsSO player;
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

    private void OnDestroy()
    {
        if (interact != null)
        {
            interact.OnInteraction -= HandleInteraction;
        }
    }

    private void HandleInteraction()
    {
        if(player != null)
        {
            player.isReady = false;
        }
    }
}
