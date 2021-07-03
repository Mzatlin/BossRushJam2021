using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BossMechanicStateMachine : MonoBehaviour
{
    public IState CurrentState { get; private set; }
    private Type nextState;
    private Dictionary<Type, IState> states;
    public float phaseHealthThreshold = 50f;
    public event Action<IState> OnStateChanged = delegate { };




    // Start is called before the first frame update
    void Start()
    {
        if(states != null)
        {
            CurrentState = states.Values.First();
            CurrentState.BeginState();
        }
    }

    public void SetStates(Dictionary<Type, IState> _states, float _phaseHealthThreshold)
    {
       // states.Clear(); //Empty and reinitialize state machine
        states = _states;
        phaseHealthThreshold = _phaseHealthThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentState != null)
        {
            nextState = CurrentState.Tick();
        }
        if (nextState != null && nextState != CurrentState?.GetType())
        {
            SwitchToNewState(nextState);
        }
    }

    void SwitchToNewState(Type nextState)
    {
        CurrentState = states[nextState];
        if (CurrentState != null)
        {
            CurrentState.BeginState();
            OnStateChanged?.Invoke(CurrentState);
        }
    }
}
