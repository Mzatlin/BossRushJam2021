using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class HitBase : MonoBehaviour, IHittablle
{
    public event Action OnHit = delegate { };
    private bool canHit = true;
    public bool CanHit { get => canHit; set => canHit = value; }

    public void ProcessDamage(float damage)
    {
        if (CanHit)
        {
            HandleHit(damage);
        }
    }

    protected virtual void HandleHit(float damage)
    {
        OnHit();
    }

}
