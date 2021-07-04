using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossIdleState :BossStateBase
{
    FirstBossAI boss;
    Type LastState;
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

    void GetRandomState()
    {
     /*   int randomPos = UnityEngine.Random.Range(0, boss.states.Count);
        Type nextType = boss.states[randomPos];
        */
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
            if (boss.CurrentBossHealth < 50)//boss.currentPhaseThreshold) //put in check health section
            {
                boss.AddToStates(typeof(BossCornerSpreadBulletPattern), new BossCornerSpreadBulletPattern(boss));
                return typeof(BossCornerSpreadBulletPattern);
            }
            else
            {
                return typeof(BossCornerSpreadBulletPattern);
            }
           
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        hasWaited = true;
    }
}
