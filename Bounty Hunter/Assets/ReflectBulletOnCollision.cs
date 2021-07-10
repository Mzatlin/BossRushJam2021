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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if((1 << collision.gameObject.layer & reflectionMasks) != 0)
        {
            var rb = collision.gameObject.GetComponent<Rigidbody2D>();
            ContactPoint2D contact = collision.contacts[0];
            if(rb != null)
            {
                var reflect = -rb.velocity;
                var projectile = collision.gameObject.GetComponent<Projectile>();
                if (projectile != null)
                {
                    projectile.SetBulletDirection(Vector2.Reflect(rb.velocity,contact.normal));
                }
                //collision.transform.rotation = Quaternion.Inverse(collision.transform.rotation);
            }

            
        }
    }
}
