using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class HitBase : MonoBehaviour, IHittablle
{
    public event Action OnHit = delegate { };

    public void ProcessDamage(float damage)
    {
        HandleHit(damage);
    }

    protected virtual void HandleHit(float damage)
    {
        OnHit();
    }

}
