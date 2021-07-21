using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MagicianDeathPhase : BossStateBase
{
    MagicianBossAI boss;
    public MagicianDeathPhase (MagicianBossAI _boss) : base(_boss.gameObject)
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
