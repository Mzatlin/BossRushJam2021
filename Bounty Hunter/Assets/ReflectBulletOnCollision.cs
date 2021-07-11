using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectBulletOnCollision : MonoBehaviour
{
    [SerializeField] LayerMask reflectionMasks;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & reflectionMasks) != 0)
        {


            Vector2 direction = Vector2.zero;
            var rb = collision.gameObject.GetComponent<Rigidbody2D>();
            var projectile = collision.gameObject.GetComponent<Projectile>();
            if (projectile != null && rb != null)
            {
                Ray2D ray = new Ray2D(collision.transform.position, rb.velocity.normalized);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 5f);
                if (hit)
                {
                    direction = Vector2.Reflect(rb.velocity, hit.normal);
                }
                projectile.SetBulletDirection(direction.normalized);
                projectile.SetBulletRotation(direction.normalized);
            }
        }
    }
}

