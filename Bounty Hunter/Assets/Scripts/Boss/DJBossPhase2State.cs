using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DJBossPhase2State : BossStateBase
{
    DJBossAI boss;

    bool isEnd;

    public DJBossPhase2State(DJBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }
    public override void BeginState()
    {
        boss.ActivateDialogue();
        //boss.HandleCoroutine(JumpTime(boss.centerPoint.position));
        boss.endDialogueEvent += HandleEnd;
        isEnd = false;
        boss.SetLasersActive(false);
        boss.transform.rotation = Quaternion.identity;
    }

    private void HandleEnd()
    {
        boss.endDialogueEvent -= HandleEnd;
        boss.SetBossTrigger("StartFlight");
        boss.SetLasersActive(false);
        boss.HandleCoroutine(Delay());
    }

    public override void EndState()
    {
        //throw new NotImplementedException();
    }

    public override Type Tick()
    {
        if (isEnd)
        {
            boss.states.Remove(typeof(DJBossPhase2State));
            boss.ResetStateMachineStates(boss.states, 25f);
            return typeof(DJBossIdleState);
        }
        else
        {
            ObjectPooler.Instance.ClearPool("Enemy Bullet 1");
            return null;
        }

    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        isEnd = true;
    }

    protected override IEnumerator JumpTime(Vector2 endPos)
    {
        yield return base.JumpTime(endPos);
        boss.ActivateDialogue();
    }
}

