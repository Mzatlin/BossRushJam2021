using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDeathCanvasOnDeath : MonoBehaviour
{
    IHealth health;
    [SerializeField] Canvas death;
    // Start is called before the first frame update
    void Awake()
    {
        health = GetComponent<IHealth>();
        if (health != null)
        {
            health.OnDie += HandleDie;
        }
        death.enabled = false;
    }

    private void OnDestroy()
    {
        if (health != null)
        {
            health.OnDie -= HandleDie;
        }
    }

    private void HandleDie()
    {
        death.enabled = true;
    }

}
