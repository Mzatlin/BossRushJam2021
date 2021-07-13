using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableBossOnDeath : MonoBehaviour
{
    [SerializeField] Slider slider;
    IHealth health;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<IHealth>();
        if(health != null)
        {
            health.OnDie += HandleDie;
        }
    }

    void OnDestroy()
    {
        if (health != null)
        {
            health.OnDie -= HandleDie;
        }
    }

    private void HandleDie()
    {
        if(slider != null)
        {
            slider.gameObject.SetActive(false);
        }
    }
}
