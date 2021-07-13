using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisableBossHealthOnStart : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject firstBoss;
    IHealth firstBossHealth;
    // Start is called before the first frame update
    void Start()
    {
        if(slider != null)
        {
            slider.gameObject.SetActive(false);
        }

        if(firstBoss != null)
        {
            firstBossHealth = firstBoss.GetComponent<IHealth>();
        }
        if(firstBossHealth != null)
        {
            firstBossHealth.OnDie += HandleDie;
        }

    }

    private void OnDestroy()
    {
        if (firstBossHealth != null)
        {
            firstBossHealth.OnDie -= HandleDie;
        }
    }

    private void HandleDie()
    {
        if (slider != null)
        {
            slider.gameObject.SetActive(true);
        }
    }
}
