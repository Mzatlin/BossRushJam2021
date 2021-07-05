using System;
using UnityEngine;

public interface IHealth 
{
    float CurrentHealth { get; set; }
    float MaxHealth { get; set; }
    bool IsDead { get; set; }
    event Action OnDie;
}
