using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationController : MonoBehaviour, IBossAnimate
{
    Animator animate;

    void Start()
    {
        animate = GetComponentInChildren<Animator>();
    }

    public void SetBossBool(string parameter, bool state)
    {
        if(animate != null)
        {
            animate.SetBool(parameter, state);
        }
    }

    public void SetBossFloat(string parameter, float amount)
    {
        if (animate != null)
        {
            animate.SetFloat(parameter, amount);
        }
    }

    public void SetBossTrigger(string trigger)
    {
        if (animate != null)
        {
            animate.SetTrigger(trigger);
        }
    }
}
