using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    [SerializeField] LayerMask collisionMasks;
    [SerializeField] float damage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if((1<<collision.gameObject.layer & collisionMasks) != 0)
        {
            var hit = collision.gameObject.GetComponent<IHittablle>();
            if (hit != null)
            {
                hit.ProcessDamage(damage);
                gameObject.SetActive(false);
            }
        }

    }
}
