using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAndShootState : BossStateBase
{
    FirstBossAI boss;
    public DashAndShootState(FirstBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }

    public override void BeginState()
    {

    }

    public override void EndState()
    {
    }

    public override Type Tick()
    {
        return null;
    }
}
