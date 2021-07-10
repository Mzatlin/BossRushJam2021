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
            if(rb != null)
            {
                var reflect = -rb.velocity;
                collision.transform.rotation = Quaternion.Inverse(collision.transform.rotation);
            }
        }
    }
}
