using UnityEngine;
using System;

public abstract class BossStateBase : IState
{
    protected GameObject bossGameObject;

    public abstract Type Tick();
    public abstract void BeginState();
    public abstract void EndState();

    public BossStateBase(GameObject _gameObject)
    {
        bossGameObject = _gameObject;
    }
}
