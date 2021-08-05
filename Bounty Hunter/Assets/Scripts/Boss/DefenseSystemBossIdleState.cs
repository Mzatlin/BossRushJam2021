using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class DefenseSystemBossIdleState : BossStateBase
{
    DefenseSystemBossAI boss;
    Type lastState;
    Type currentState;
    bool hasWaited = false;
    bool hasStartedWait = false;
    bool isOpening = false;

    public DefenseSystemBossIdleState(DefenseSystemBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }

    public override void BeginState()
    {
        hasWaited = false;
        hasStartedWait = false;
        if (!boss.GetOpeningStats())
        {
            isOpening = true;
        }
        else
        {
            isOpening = false;
        }
    }

    public override void EndState()
    {
        Debug.Log("Exited Idle State");
    }

    public override Type Tick()
    {
        if (isOpening)
        {
            return typeof(SecondBossOpeningState);
        }
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
            return currentState;
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
        currentState = GetRandomState();
    }
}
