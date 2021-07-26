using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvulnerabiltyTime : MonoBehaviour
{
    IHittablle hit;
    IHealth health;
    [SerializeField] float hitDelay = 1f;
    Coroutine invunlerability;
    // Start is called before the first frame update
    void Start()
    {
        hit = GetComponent<IHittablle>();
        if(hit != null)
        {
            hit.OnHit += HandleHit;
        }
        health = GetComponent<IHealth>();
    }

    void OnDestroy()
    {
        hit.OnHit -= HandleHit;    
    }

    private void HandleHit()
    {
        if(health.CurrentHealth >= 1)
        {
            hit.CanHit = false;
            invunlerability = StartCoroutine(InvulnerabilityTime());
        }
    }

    IEnumerator InvulnerabilityTime()
    {
        yield return new WaitForSeconds(hitDelay);
        hit.CanHit = true;
    }

}
