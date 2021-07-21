using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MagicianBossPhase2State : BossStateBase
{
    MagicianBossAI boss;

    bool isEnd;

    public MagicianBossPhase2State(MagicianBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }
    public override void BeginState()
    {
        boss.HandleCoroutine(RespawnTime());
        boss.endDialogueEvent += HandleEnd;
        isEnd = false;
        boss.GetLineRenderer().enabled = false;
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
            boss.states.Remove(typeof(MagicianBossPhase2State));
            boss.ResetStateMachineStates(boss.states, 25f);
            boss.FireAllMirrors();
            return typeof(MagicianIdleState);
        }
        else
        {
            return null;
        }

    }


    IEnumerator RespawnTime()
    {
        boss.EnableBoss(true);
        yield return new WaitForSeconds(0.3f);
        boss.ActivateDialogue();
    }
}