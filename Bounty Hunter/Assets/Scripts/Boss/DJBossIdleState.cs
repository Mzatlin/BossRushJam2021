using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DJBossIdleState : BossStateBase
{
    DJBossAI boss;
    Type lastState;
    Type currentState;
    bool hasWaited = false;
    bool hasStartedWait = false;

    public DJBossIdleState(DJBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }

    public override void BeginState()
    {
        Debug.Log("Entered Idle State");
        hasWaited = false;
        hasStartedWait = false;
    }

    public override void EndState()
    {
        Debug.Log("End of Idle State");
    }

    public override Type Tick()
    {
        if (!hasWaited)
        {
            if (!hasStartedWait)
            {
                hasStartedWait = true;
                boss.HandleCoroutine(Delay());
            }

            return null;
        }
        else
        {
            return typeof(DJBossIdleState);
        }
    }

    Type GetRandomState()
    {
        Type randomType = boss.states.Keys.ElementAt(UnityEngine.Random.Range(1, boss.states.Keys.Count));
        if (lastState == null || lastState != randomType)
        {
            lastState = randomType;
            return lastState;
        }
        else
        {
            return GetRandomState();
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        hasWaited = true;
        //currentState = GetRandomState();
    }
}
