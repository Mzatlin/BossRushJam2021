using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class FirstBossOpeningState : BossStateBase
{
    FirstBossAI boss;
    bool isEnd;
    public FirstBossOpeningState(FirstBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }
    public override void BeginState()
    {
        boss.HandleCoroutine(Delay());
        boss.endDialogueEvent += HandleEnd;
        isEnd = false;
    }

    private void HandleEnd()
    {
        boss.endDialogueEvent -= HandleEnd;
        boss.HandleCoroutine(EndDelay());
    }

    public override void EndState()
    {
        isEnd = false;
    }
    IEnumerator EndDelay()
    {
        yield return new WaitForSeconds(3f);
        isEnd = true;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        boss.ActivateDialogue();
    }

    public override Type Tick()
    {
        if (isEnd)
        {
            boss.states.Remove(typeof(FirstBossOpeningState));
            boss.ResetStateMachineStates(boss.states, 25f);
            boss.SetOpeningStats(true);
            return typeof(FirstBossIdleState);
        }
        else
        {
            return null;
        }
    }
}

