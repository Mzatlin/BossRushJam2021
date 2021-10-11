using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCameraOnHit : MonoBehaviour
{
    IHittablle hit;
    IHealth health;
    ICameraShake shake;
    Camera cam;
    [SerializeField]
    float shakeMagnitude = 0.5f;
    [SerializeField]
    float shakeDuration = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        hit = GetComponent<IHittablle>();
        if (hit != null)
        {
            hit.OnHit += HandleHit;
        }
        health = GetComponent<IHealth>();
        shake = GetComponent<ICameraShake>();
        cam = Camera.main;
        shake = cam.GetComponent<ICameraShake>();
    }

    void OnDestroy()
    {
        hit.OnHit -= HandleHit;
    }

    private void HandleHit()
    {
        if (!health.IsDead && shake != null)
        {
            shake.TryShake(shakeDuration,shakeMagnitude);
        }
    }

}
