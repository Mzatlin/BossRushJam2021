using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BossAIBase : MonoBehaviour
{
    public BossMechanicStateMachine StateMachine => GetComponent<BossMechanicStateMachine>();
    public Dictionary<Type, IState> states;
    ActivateDialogueFromBossAI dialogue;

    [SerializeField] protected GameObject player;
    [SerializeField] protected LayerMask obstacleLayers;
    protected Coroutine enemyCoroutine;


    protected virtual void Awake()
    {
        dialogue = GetComponent<ActivateDialogueFromBossAI>();
        InitializeStateMachine();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected abstract void InitializeStateMachine();

    public void HandleCoroutine(IEnumerator routine)
    {
        if (routine != null)
        {
            enemyCoroutine = StartCoroutine(routine);
        }
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public void ResetStateMachineStates(Dictionary<Type, IState> states, float healthThreshold)
    {
        StateMachine.SetStates(states, healthThreshold);
    }

    public void AddToStates(Type type, IState state)
    {
        if (!states.ContainsKey(type))
        {
            states.Add(type, state);
        }
    }

    public void ActivateDialogue()
    {
        dialogue.ActivateDialogue();
    }
}
