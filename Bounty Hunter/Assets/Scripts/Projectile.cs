using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 20f;
    public float lifetime = 2f;
    float currentTime;
    Rigidbody2D rb;

    void Awake()
    {
        currentTime = Time.time + lifetime;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        currentTime = Time.time + lifetime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Time.time > currentTime)
        {
            gameObject.SetActive(false);
        }
        else
        {
            rb.velocity = transform.right * projectileSpeed * Time.fixedDeltaTime;
        }


    }
}
