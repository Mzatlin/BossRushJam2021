using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnDetonate : MonoBehaviour
{
    IDetonate detonate;
    SpriteRenderer sprite;
    ParticleSystem explosionParticle;
    [SerializeField] float explosionDelay;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask damageLayer;
    [SerializeField] float damageAmount = 1f;

    // Start is called before the first frame update
    void Start()
    {
        detonate = GetComponent<IDetonate>();   
        if(detonate != null)
        {
            detonate.OnDetonate += HandleDetonation;
        }
        sprite = GetComponentInChildren<SpriteRenderer>();
        explosionParticle = GetComponentInChildren<ParticleSystem>();
    }

    void OnDestroy()
    {
        if(detonate != null)
        {
            detonate.OnDetonate -= HandleDetonation;
        }
    }

    private void OnDisable()
    {
        if (detonate != null)
        {
            detonate.OnDetonate -= HandleDetonation;
        }
    }

    private void HandleDetonation()
    {
        StartCoroutine(ExplosionDelay());   
    }

    void DamageOnExplosion()
    {
       Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position,attackRange,damageLayer);
        foreach(Collider2D hit in hitEnemies)
        {
            var damage = hit.GetComponent<IHittablle>();
            if(damage != null)
            {
                damage.ProcessDamage(damageAmount);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    IEnumerator ExplosionDelay()
    {
        yield return new WaitForSeconds(explosionDelay);
        if(sprite != null)
        {
            sprite.enabled = false;
        }
        if(explosionParticle != null)
        {
            explosionParticle.Play();
        }
        DamageOnExplosion();
        StartCoroutine(DeactivateDelay());
    }

    IEnumerator DeactivateDelay()
    {
        yield return new WaitForSeconds(0.7f);
        gameObject.SetActive(false);
    }
}
