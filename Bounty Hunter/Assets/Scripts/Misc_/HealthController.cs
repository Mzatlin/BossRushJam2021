using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthController : HitBase, IHealth
{
    public event Action OnDie = delegate { };

    [SerializeField]
    float currentHealth = 1f;
    [SerializeField]
    float maxHealth = 1f;
    [SerializeField]
    bool isDead = false;

    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public bool IsDead { get => isDead; set => isDead = value; }

    //health is decremented and death is handled here. Unless already dead, the hit event will be triggered. 
    protected override void HandleHit(float damage)
    {
        if (isDead)
        {
            CanHit = false;
            return;
        }

        base.HandleHit(damage);
        currentHealth -= damage;

        if (currentHealth < 1)
        {
            isDead = true;
            OnDie();
        }
    }
}
