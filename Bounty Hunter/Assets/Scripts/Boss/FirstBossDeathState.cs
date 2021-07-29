using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstBossDeathState : BossStateBase
{
    FirstBossAI boss;
    public FirstBossDeathState(FirstBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }
    public override void BeginState()
    {
        ObjectPooler.Instance.ClearPool("Enemy Bullet 1");
        boss.HandleCoroutine(Delay());
    }

    public override void EndState()
    {
        throw new NotImplementedException();
    }

    IEnumerator Delay()
    {
        boss.IncrementCurrentDay();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

    public override Type Tick()
    {
        return null;
    }
}
