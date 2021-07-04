using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    IHealth health;
    // Start is called before the first frame update
    void Awake()
    {
        health = GetComponent<IHealth>();
        if(health != null && healthSlider != null)
        {
            healthSlider.maxValue = health.MaxHealth;
            healthSlider.value = health.CurrentHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health != null && healthSlider != null)
        {
            healthSlider.value = health.CurrentHealth;
        }

    }
}
