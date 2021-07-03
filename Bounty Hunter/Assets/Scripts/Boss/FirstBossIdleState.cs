using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossIdleState :BossStateBase
{
    FirstBossAI boss;
    bool hasWaited = false;
    public FirstBossIdleState(FirstBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }

    public override void BeginState()
    {
        Debug.Log("Entered Idle State");
        hasWaited = false;
    }

    public override void EndState()
    {
        throw new NotImplementedException();
    }

    public override Type Tick()
    {
        if (!hasWaited)
        {
            boss.HandleCoroutine(Delay());
            return null;
        }
        else
        {
            return typeof(BossChargeState);
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        hasWaited = true;
    }
}
