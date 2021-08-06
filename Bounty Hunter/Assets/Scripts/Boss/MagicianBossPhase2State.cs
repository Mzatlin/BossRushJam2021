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
            ObjectPooler.Instance.ClearPool("Enemy Bullet 1");
            return null;
        }

    }

    IEnumerator Delay()
    {
        boss.SetBossTrigger("WarpOut");
        boss.SetFlashAnimation();
        yield return new WaitForSeconds(0.35f);
        boss.EnableBoss(false);
        boss.states.Remove(typeof(MagicianBossPhase2State));
        boss.ResetStateMachineStates(boss.states, 25f);
        boss.SetHalfMirrorsActive(true);
        boss.ResetMirrors();
        //boss.FireAllMirrors();
        isEnd = true;
    }

    IEnumerator RespawnTime()
    {
        boss.EnableBoss(true);
        yield return new WaitForSeconds(0.3f);
        boss.ActivateDialogue();
    }
}
