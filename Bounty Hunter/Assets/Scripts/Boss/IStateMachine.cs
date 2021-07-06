using System.Collections.Generic;
using System;

public interface IStateMachine
{
    void SetStates(Dictionary<Type, IState> _states, float _phaseHealthThreshold);
    void SwitchToNewState(Type state);
}
