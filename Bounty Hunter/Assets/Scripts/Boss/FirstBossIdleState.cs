using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FirstBossIdleState : BossStateBase
{
    FirstBossAI boss;
    Type lastState;
    Type currentState;
    bool hasWaited = false;
    bool isJumping = false;
    bool isOpening = false;
    public FirstBossIdleState(FirstBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }

    public override void BeginState()
    {
        Debug.Log("Entered Idle State");
        hasWaited = false;
        isJumping = false;
        boss.SetGunVisibility(false);
        if (!boss.GetOpeningStats())
        {
            isOpening = true;
        }
        else
        {
            isOpening = false;
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

    public override void EndState()
    {
        throw new NotImplementedException();
    }

    public override Type Tick()
    {
        if (isOpening)
        {
            return typeof(FirstBossOpeningState);
        }
        if (isJumping == false)
        {
            isJumping = true;
            boss.HandleCoroutine(JumpTime(boss.centerPoint.position));
        }


        if (!hasWaited)
        {
            return null;
        }
        else
        {
            return currentState;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        hasWaited = true;
        currentState = GetRandomState();
    }

    protected override IEnumerator JumpTime(Vector2 endPos)
    {
        if (Mathf.Abs(boss.GetRigidBody().velocity.x) > 0.1f)
        {
            boss.SetBossTrigger("Dash");
        }
        yield return base.JumpTime(endPos);
        boss.HandleCoroutine(Delay());
    }
}
