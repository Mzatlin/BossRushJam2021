using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessInteractionController : MonoBehaviour, IInteract
{
    bool isInteracting = false;
    public bool IsCurrentingInteracting { get => isInteracting; set => isInteracting = value; }

    public event Action OnInteraction = delegate { };

    public void ProcessInteraction()
    {
        OnInteraction();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
