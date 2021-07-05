using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDie : MonoBehaviour
{
    IHealth health;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<IHealth>();  
        if(health != null)
        {
            health.OnDie += HandleDie;
        }
    }

    private void OnDestroy()
    {
        if(health != null)
        {
            health.OnDie -= HandleDie;
        }
    }

    private void HandleDie()
    {
        Destroy(gameObject);
    }
}
