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
        boss.HandleCoroutine(JumpTime(boss.centerPoint.position));
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
        isEnd = false;
    }

    public override Type Tick()
    {
        if (isEnd)
        {
            boss.SetBossTrigger("SpawnLandMine");
            boss.HandleCoroutine(SpawnLandMines(15));
            boss.states.Remove(typeof(FirstBossPhase2State));
            boss.SetFlashAnimation();
            boss.ResetStateMachineStates(boss.states, 25f);
            return typeof(FirstBossIdleState);
        }
        else
        {
            ObjectPooler.Instance.ClearPool("Enemy Bullet 1");
            return null;
        }

    }

    IEnumerator SpawnLandMines(int mineAmount)
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < mineAmount; i++)
        {
            GameObject landMine = boss.CreateLandMine(new Vector2(UnityEngine.Random.Range(-7, 10), UnityEngine.Random.Range(-3, 6)));
            yield return null;
        }
    }

    protected override IEnumerator JumpTime(Vector2 endPos)
    {
        boss.SetDash(true);
        yield return base.JumpTime(endPos);
        boss.SetDash(false);
        boss.ActivateDialogue();
    }
}
