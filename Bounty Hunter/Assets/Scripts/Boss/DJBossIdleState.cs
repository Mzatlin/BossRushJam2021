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
    int randomJumpAmount = 2;

    public DJBossIdleState(DJBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }

    public override void BeginState()
    {
        Debug.Log("Entered Idle State");
        hasWaited = false;
        hasStartedWait = false;
        randomJumpAmount = UnityEngine.Random.Range(2, 4);
    }

    public override void EndState()
    {
        Debug.Log("End of Idle State");
    }

    public override Type Tick()
    {
        if (!hasWaited)
        {
            if (!hasStartedWait && randomJumpAmount >= 1)
            {
                hasStartedWait = true;
                Vector2 randomPos = boss.bossIdleLocations[UnityEngine.Random.Range(1, boss.bossIdleLocations.Length)].position;
                Debug.Log("Boss Idle Moved to: "+randomPos);
                boss.HandleCoroutine(JumpTime(randomPos));
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
        hasStartedWait = false;
        randomJumpAmount--;
        if(randomJumpAmount < 1)
        {
            hasWaited = true;
        }
    }

    protected override IEnumerator JumpTime(Vector2 endPos)
    {
        yield return base.JumpTime(endPos);
        boss.HandleCoroutine(Delay());
    }
}
