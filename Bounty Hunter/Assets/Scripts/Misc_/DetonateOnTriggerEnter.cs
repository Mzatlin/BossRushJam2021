using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetonateOnTriggerEnter : MonoBehaviour, IDetonate
{
    [SerializeField] LayerMask touchLayers;
    public event Action OnDetonate = delegate { };
    Collider2D detonateCollider;

    private void Start()
    {
        detonateCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((1 << collision.gameObject.layer & touchLayers) != 0)
        {
            OnDetonate();
            if(detonateCollider != null)
            {
                detonateCollider.enabled = false;
            }
        }
    }
}
