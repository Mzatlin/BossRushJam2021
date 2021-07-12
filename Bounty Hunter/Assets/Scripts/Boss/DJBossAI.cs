using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DJBossAI : BossAIBase
{
    public List<LineRenderer> lasers = new List<LineRenderer>();

    protected override void InitializeStateMachine()
    {
        states = new Dictionary<Type, IState>()
        {
            {typeof(DJBossIdleState), new DJBossIdleState(this) },
             {typeof(LaserSpinState), new LaserSpinState(this) }
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
