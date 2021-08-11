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
    IStateMachine stateMachine;
    Collider2D collider => GetComponent<Collider2D>();
    DJBossAI DJ;
    // Start is called before the first frame update
    void Start()
    {
        DJ = GetComponent<DJBossAI>();
        if (slider != null)
        {
            slider.gameObject.SetActive(false);
        }

        if (firstBoss != null)
        {
            firstBossHealth = firstBoss.GetComponent<IHealth>();
        }
        if (firstBossHealth != null)
        {
            firstBossHealth.OnDie += HandleDie;
        }

        stateMachine = GetComponent<IStateMachine>();
        if (collider != null)
        {
            collider.enabled = false;
        }
        TogglePause();
    }

    void TogglePause()
    {
        if (stateMachine != null)
        {
            stateMachine.PauseStateMachine();
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
            TogglePause();
            if (collider != null)
            {
                collider.enabled = true;
            }
            if (DJ != null)
            {
                DJ.StartDJBoss();
            }
        }
    }
}
