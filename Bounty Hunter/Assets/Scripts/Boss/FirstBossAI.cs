using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FirstBossAI : MonoBehaviour
{
    IHealth health => GetComponent<IHealth>();
    public float CurrentBossHealth => health.CurrentHealth;
    public BossMechanicStateMachine StateMachine => GetComponent<BossMechanicStateMachine>();
    LineRenderer lineRender;
    public event Action<Collider2D> hitEvent = delegate { };

    Coroutine enemyCoroutine;

    public float currentPhaseThreshold = 50f;

    public int currentPhase = 1;

    [SerializeField] GameObject player;
    [SerializeField] Transform centerPoint;
    [SerializeField] LayerMask obstacleLayers;
    [SerializeField] GameObject bullet;

    Dictionary<Type, IState> states;

    // Start is called before the first frame update
    void Awake()
    {
        lineRender = GetComponent<LineRenderer>();
        InitializeStateMachine();
    }

    void InitializeStateMachine()
    {
        states = new Dictionary<Type, IState>()
        {
            {typeof(FirstBossIdleState), new FirstBossIdleState(this) },
            {typeof(BossChargeState), new BossChargeState(this) }
        };

        ResetStateMachineStates(states, 50);
    }

    public void ResetStateMachineStates(Dictionary<Type, IState> states, float healthThreshold)
    {
        StateMachine.SetStates(states, healthThreshold);
    }

    public LineRenderer GetLineRenderer()
    {
        return lineRender;
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public Transform GetCenterPosition()
    {
        return centerPoint;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleCoroutine(IEnumerator routine)
    {
        if (routine != null)
        {
            enemyCoroutine = StartCoroutine(routine);
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & obstacleLayers) != 0)
        {
            hitEvent(collision);
        }
    }

    public void AddToStates(Type type, IState state)
    {
        if (!states.ContainsKey(type))
        {
            states.Add(type, state);
        }
    }

    public GameObject CreateBullet(Vector3 startPos, Quaternion rotation)
    {
        return Instantiate(bullet, startPos, rotation);
    }
}