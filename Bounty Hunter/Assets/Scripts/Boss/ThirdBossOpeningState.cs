using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThirdBossOpeningState : BossStateBase
{
    MagicianBossAI boss;
    bool isEnd;
    public ThirdBossOpeningState(MagicianBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }
    public override void BeginState()
    {
        Debug.Log("entered opening");
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
        throw new NotImplementedException();
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
            boss.states.Remove(typeof(ThirdBossOpeningState));
            boss.ResetStateMachineStates(boss.states, 25f);
            boss.SetOpeningStats(true);
            return typeof(MagicianIdleState);
        }
        else
        {
            return null;
        }
    }
}
