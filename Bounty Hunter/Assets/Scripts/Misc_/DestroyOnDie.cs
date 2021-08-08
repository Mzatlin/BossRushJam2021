using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDie : MonoBehaviour
{
    IHealth health;
    SpriteRenderer render;
    ParticleSystem explosionParticle;
    AudioManager audio;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<IHealth>();  
        if(health != null)
        {
            health.OnDie += HandleDie;
        }
        render = GetComponentInChildren<SpriteRenderer>();
        render.enabled = true;
        explosionParticle = GetComponentInChildren<ParticleSystem>();
        audio = FindObjectOfType<AudioManager>();
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
        StartCoroutine(Delay());
    }

   IEnumerator Delay()
    {
        if(render != null)
        {
            render.enabled = false;
        }
        if (explosionParticle != null)
        {
            explosionParticle.Play();
        }
        if (audio != null)
        {
            audio.PlayAudioByString("Play_BombExplosion", gameObject);
        }
        yield return new WaitForSeconds(0.7f);
        gameObject.SetActive(false);
    }
}
