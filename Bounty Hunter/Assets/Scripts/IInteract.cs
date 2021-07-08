using System;
interface IInteract
{
    event Action OnInteraction;
    void ProcessInteraction();
    bool IsCurrentingInteracting { get; set; }
}