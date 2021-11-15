using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerDeath : MonoBehaviour
{
    IHealth health;
    IPlayerStats stats;
    SpriteRenderer sprite;
    Collider2D playerCollider;
    [SerializeField] GameObject gun;
    Animator animate;
    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        health = GetComponent<IHealth>();
        if(health != null)
        {
            health.OnDie += HandleDie;
        }
        stats = GetComponent<IPlayerStats>();
        animate = GetComponentInChildren<Animator>();
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
            stats.SetPlayerDeath(true);
        }
       if(gun != null)
        {
            gun.SetActive(false);
        }
       if(sprite != null)
        {
            sprite.enabled = false;
        }
       if(animate != null)
        {
            animate.SetTrigger("Dead");
        }
    }

}
