using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectBulletOnCollision : MonoBehaviour
{
    [SerializeField] LayerMask reflectionMasks;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if((1 << collision.gameObject.layer & reflectionMasks) != 0)
        {
            var rb = collision.gameObject.GetComponent<Rigidbody2D>();
            //ContactPoint2D contact = collision.contacts[0];
            if(rb != null)
            {
                var reflect = new Vector2(-rb.velocity.x, -rb.velocity.y);
                var projectile = collision.gameObject.GetComponent<Projectile>();
                if (projectile != null)
                {
                    Vector2 direction = reflect;//Vector2.Reflect(-rb.velocity, contact.normal);
                    Ray2D ray = new Ray2D(collision.transform.position, rb.velocity.normalized);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 5f);
                    if (hit)
                    {
                        direction = Vector2.Reflect(rb.velocity, hit.normal);
                    }
                    projectile.SetBulletDirection(direction.normalized);
                }
                //collision.transform.rotation = Quaternion.Inverse(collision.transform.rotation);
            }

            
        }
    }
}
