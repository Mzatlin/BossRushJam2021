using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossPhase3State : BossStateBase
{
    FirstBossAI boss;
    bool isEnd = false;
    public FirstBossPhase3State(FirstBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
        boss.endDialogueEvent -= HandleEnd;
        isEnd = true;
    }

    private void HandleEnd()
    {
        boss.endDialogueEvent -= HandleEnd;
        isEnd = true;
    }

    public override void BeginState()
    {
        boss.HandleCoroutine(JumpTime(boss.centerPoint.position));
        boss.endDialogueEvent += HandleEnd;
        isEnd = false;
        boss.GetLineRenderer().enabled = false;
    }

    public override void EndState()
    {
        throw new NotImplementedException();
    }

    public override Type Tick()
    {

        if (isEnd)
        {
            boss.SetBossTrigger("SpawnLandMine");
            boss.HandleCoroutine(SpawnDrones(4));
            boss.states.Remove(typeof(FirstBossPhase3State));
            boss.ResetStateMachineStates(boss.states, 25f);
            return typeof(FirstBossIdleState);
        }
        else
        {
            return null;
        }
    }

    IEnumerator SpawnDrones(int mineAmount)
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < mineAmount; i++)
        {
            GameObject landMine = boss.CreateDrone(i < boss.bossLocations.Length ? boss.bossLocations[i].position : boss.bossLocations[0].position);
            yield return null;
        }
    }

    protected override IEnumerator JumpTime(Vector2 endPos)
    {
        yield return base.JumpTime(endPos);
        boss.ActivateDialogue();
    }

}
