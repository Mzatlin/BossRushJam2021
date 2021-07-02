using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth 
{
    float CurrentHealth { get; set; }
    float MaxHealth { get; set; }
    bool IsDead { get; set; }
}
