using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossPhase2State : BossStateBase
{
    FirstBossAI boss;
    bool isEnd;

    public FirstBossPhase2State(FirstBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }
    public override void BeginState()
    {
        boss.HandleCoroutine(jumpTime(boss.centerPoint.position));
        boss.endDialogueEvent += HandleEnd;
        isEnd = false;
    }

    private void HandleEnd()
    {
        boss.endDialogueEvent -= HandleEnd;
        isEnd = true;
    }

    public override void EndState()
    {
        throw new NotImplementedException();
    }

    public override Type Tick()
    {
        if (isEnd)
        {
            boss.states.Remove(typeof(FirstBossPhase2State));
            boss.ResetStateMachineStates(boss.states, 25f);
            return typeof(FirstBossIdleState);
        }
        else
        {
            return null;
        }

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
        boss.ActivateDialogue();
    }
}
