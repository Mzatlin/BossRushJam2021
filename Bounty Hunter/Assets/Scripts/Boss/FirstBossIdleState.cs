using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FirstBossIdleState :BossStateBase
{
    FirstBossAI boss;
    Type lastState;
    Type currentState;
    bool hasWaited = false;
    bool isJumping = false;
    public FirstBossIdleState(FirstBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }

    public override void BeginState()
    {
        Debug.Log("Entered Idle State");
        hasWaited = false;
        currentState = GetRandomState();
    }

    Type GetRandomState()
    {
        Type randomType = boss.states.Keys.ElementAt(UnityEngine.Random.Range(1, boss.states.Keys.Count));
        if(lastState == null || lastState != randomType)
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
        if(isJumping == false)
        {
            isJumping = true;
            boss.HandleCoroutine(jumpTime(boss.centerPoint.position));
        }


        if (!hasWaited)
        {
            boss.HandleCoroutine(Delay());
            return null;
        }
        else
        {
            if (boss.CurrentBossHealth < 50)//boss.currentPhaseThreshold) //put in check health section
            {
                //boss.AddToStates(typeof(BossCornerSpreadBulletPattern), new BossCornerSpreadBulletPattern(boss));
                //return typeof(BossCornerSpreadBulletPattern);
                return currentState;
            }
            else
            {
             
                return currentState;
            }
           
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        hasWaited = true;
    }

    IEnumerator jumpTime(Vector2 endPos)
    {

        float lerpSpeed = 30f;
        Vector2 startPos = boss.transform.position;
        float totalDistance = Vector2.Distance(startPos, endPos);
        float fractionOfJourney = 0;
        float startTime = Time.time;

        while (fractionOfJourney < 1)
        {
            fractionOfJourney = ((Time.time - startTime) * lerpSpeed) / totalDistance;
            boss.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }

    }
}
