using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DJBossAI : BossAIBase
{
    public List<LineRenderer> lasers = new List<LineRenderer>();
    public Transform[] bossLocations;
    public Transform[] bossIdleLocations;
    [SerializeField] GameObject firstBoss;
    IStateMachine firstBossState;

    bool isUnpaused = false; 
    protected override void InitializeStateMachine()
    {
        states = new Dictionary<Type, IState>()
        {
            {typeof(DJBossIdleState), new DJBossIdleState(this) },
        };

        ResetStateMachineStates(states, 50);
        SetupLineRenderers();
    }

    void SetupLineRenderers()
    {
        foreach (Transform obj in transform)
        {
            var render = obj.gameObject.GetComponent<LineRenderer>();
            if (render != null)
            {
                lasers.Add(render);
            }
        }
    }

    public LayerMask GetObstacleMask()
    {
        return obstacleLayers;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(firstBoss != null)
        {
            firstBossState = firstBoss.GetComponent<IStateMachine>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentBossHealth < 50 && firstBossState != null && !isUnpaused)
        {
            firstBossState.PauseStateMachine();
            isUnpaused = true;
        }
    }
}
