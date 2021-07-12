using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpinState : BossStateBase
{
    FirstBossAI boss;

    public LaserSpinState(FirstBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }
    public override void BeginState()
    {
        throw new NotImplementedException();
    }

    public override void EndState()
    {
        throw new NotImplementedException();
    }

    public override Type Tick()
    {
        throw new NotImplementedException();
    }
}
