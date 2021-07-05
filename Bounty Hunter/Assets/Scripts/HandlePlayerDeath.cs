using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerDeath : MonoBehaviour
{
    IHealth health;
    IPlayerStats stats;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<IHealth>();
        if(health != null)
        {
            health.OnDie += HandleDie;
        }
        stats = GetComponent<IPlayerStats>();
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
       if(stats != null)
        {
            stats.SetPlayerReadiness(false);
            //set player death as well.
        }
    }

}
