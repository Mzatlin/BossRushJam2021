using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BossMechanicStateMachine : MonoBehaviour, IStateMachine
{
    public IState CurrentState { get; private set; }
    private Type nextState;
    private Dictionary<Type, IState> states;
    private bool isPaused = false;
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
        states = _states;
        phaseHealthThreshold = _phaseHealthThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused)
        {
            return;
        }

        if (CurrentState != null)
        {
            nextState = CurrentState.Tick();
        }
        if (nextState != null && nextState != CurrentState?.GetType())
        {
            SwitchToNewState(nextState);
        }
    }

    public void SwitchToNewState(Type nextState)
    {
        CurrentState = states[nextState];
        if (CurrentState != null)
        {
            CurrentState.BeginState();
            OnStateChanged?.Invoke(CurrentState);
        }
    }

    public void PauseStateMachine()
    {
        if(CurrentState != null && !isPaused)
        {
            CurrentState.EndState();
        }
        CurrentState = states.Values.First(); //first element is always the idlestate.
        isPaused = !isPaused;
        if (CurrentState != null)
        {
            CurrentState.BeginState();
            OnStateChanged?.Invoke(CurrentState);
        }
    }
}
