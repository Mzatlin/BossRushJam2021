using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MagicianBossPhase3 : BossStateBase
{
    MagicianBossAI boss;

    bool isEnd;

    public MagicianBossPhase3(MagicianBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }
    public override void BeginState()
    {
        boss.HandleCoroutine(RespawnTime());
        boss.endDialogueEvent += HandleEnd;
        isEnd = false;
    }

    private void HandleEnd()
    {
        boss.endDialogueEvent -= HandleEnd;
        boss.HandleCoroutine(Delay());
    }

    public override void EndState()
    {
        throw new NotImplementedException();
    }

    public override Type Tick()
    {
        if (isEnd)
        {
            return typeof(MagicianIdleState);
        }
        else
        {
            return null;
        }

    }

    IEnumerator Delay()
    {
        boss.SetBossTrigger("WarpOut");
        yield return new WaitForSeconds(0.35f);
        boss.EnableBoss(false);
        boss.states.Remove(typeof(MagicianBossPhase3));
        boss.ResetStateMachineStates(boss.states, 25f);
        boss.SetHalfMirrorsActive(true);
        boss.ResetMirrors();
        boss.FireAllMirrors();
        yield return new WaitForSeconds(5f);
        isEnd = true;
    }

    IEnumerator RespawnTime()
    {
        boss.EnableBoss(true);
        boss.SetBossTrigger("WarpIn");
        yield return new WaitForSeconds(0.65f);
        boss.ActivateDialogue();
    }
}
