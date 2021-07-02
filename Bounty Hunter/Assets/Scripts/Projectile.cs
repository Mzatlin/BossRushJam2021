using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 20f;
    float lifetime = 5f;
    float currentTime;
    Rigidbody2D rb;

    void Awake()
    {
        currentTime = Time.time + lifetime;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Time.time > currentTime)
        {
            Destroy(gameObject);
        }
        else
        {
            rb.velocity = transform.right * projectileSpeed * Time.fixedDeltaTime;
        }


    }
}
