using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableBossOnDeath : MonoBehaviour
{
    [SerializeField] Slider slider;
    IHealth health;
    IStateMachine state;
    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<IStateMachine>();
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
        ObjectPooler.Instance.ClearPool("Enemy Bullet 1");
        if (slider != null && state != null)
        {
            slider.gameObject.SetActive(false);
            state.SwitchToNewState(typeof(DefenseSystemBossIdleState));
            state.PauseStateMachine();
            var boss = GetComponent<BossAIBase>();
            if(boss != null)
            {
                boss.HandleStopCoroutine();
                boss.SetBossTrigger("Death");
                boss.SetLasers(false);
            }
        }
    }
}
