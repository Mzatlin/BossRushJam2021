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
    Vector2 bulletDirection;
    Quaternion bulletRotation;

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
        if (Time.time > currentTime)
        {
            gameObject.SetActive(false);
        }
        else
        {
            rb.velocity = bulletDirection * projectileSpeed * Time.fixedDeltaTime;
        }
    }

    public void SetBulletDirection(Vector2 direction)
    {
        bulletDirection = direction;
    }

    public void SetBulletRotation(Vector2 direction)
    {
        float bulletangle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletRotation.eulerAngles = new Vector3(0, 0, bulletangle);
        transform.rotation = bulletRotation;
    }
}
