using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class DJBossDeathState : BossStateBase
{
    DJBossAI boss;
    public DJBossDeathState(DJBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }
    public override void BeginState()
    {
        SceneManager.LoadScene(1);
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