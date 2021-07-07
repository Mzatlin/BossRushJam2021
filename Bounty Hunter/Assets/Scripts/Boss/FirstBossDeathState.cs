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
        SceneManager.LoadScene(0);
    }

    public override void EndState()
    {
        throw new NotImplementedException();
    }

    public override Type Tick()
    {
        return null;
    }
}
