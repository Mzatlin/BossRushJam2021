using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvulnerabiltyTime : MonoBehaviour
{
    IHittablle hit;
    IHealth health;
    SpriteRenderer sprite;
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
        sprite = GetComponentInChildren<SpriteRenderer>();
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
        float timeDelay = Time.time + hitDelay;
        while (Time.time < timeDelay)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.05f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }
        sprite.enabled = true;
        hit.CanHit = true;
        yield return null;
    }

}
