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
    Vector2 lastPos;

    public DJBossIdleState(DJBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }

    public override void BeginState()
    {
        boss.SetBossBool("IsFiring", false);
        boss.SetBossTrigger("DJFloat");
        hasWaited = false;
        hasStartedWait = false;
        randomJumpAmount = UnityEngine.Random.Range(2, 4);
        boss.SetLasers(false);
    }

    public override void EndState()
    {
        hasWaited = false;
        hasStartedWait = false;
    }


    public override Type Tick()
    {
        if (!hasWaited)
        {
            if (!hasStartedWait && randomJumpAmount >= 1)
            {
                hasStartedWait = true;
                Vector2 randomPos = GetRandomPosition();
                boss.HandleCoroutine(JumpTime(randomPos));
            }

            return null;
        }
        else
        {
            return currentState;
        }
    }


    Vector2 GetRandomPosition()
    {
        int randomPos = UnityEngine.Random.Range(0, boss.bossIdleLocations.Length);
        Vector2 nextPos = boss.bossIdleLocations[randomPos].position;
        if (lastPos == null || nextPos != lastPos)
        {
            lastPos= nextPos;
            return lastPos;
        }
        else
        {
            return GetRandomPosition();
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
        yield return new WaitForSeconds(1f);
        hasStartedWait = false;
        randomJumpAmount--;
        if(randomJumpAmount < 1)
        {
            hasWaited = true;
            currentState = GetRandomState();
        }
    }

    protected override IEnumerator JumpTime(Vector2 endPos)
    {
        yield return base.JumpTime(endPos);
        boss.HandleCoroutine(Delay());
    }
}
