using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FirstBossAI : MonoBehaviour
{
    IHealth health => GetComponent<IHealth>();
    IDialogueEnd dialogueEnd => GetComponent<IDialogueEnd>();
    IBossAnimate animate => GetComponent<IBossAnimate>();
    public float CurrentBossHealth => health.CurrentHealth;
    ActivateDialogueFromBossAI dialogue;
    public BossMechanicStateMachine StateMachine => GetComponent<BossMechanicStateMachine>();
    LineRenderer lineRender;
    public event Action<Collider2D> hitEvent = delegate { };
    public event Action endDialogueEvent = delegate { };

    Coroutine enemyCoroutine;

    public float currentPhaseThreshold = 50f;

    public int currentPhase = 1;

    [SerializeField] GameObject player;
    public Transform centerPoint;
    [SerializeField] LayerMask obstacleLayers;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject landMine;
    public Transform[] bossLocations;

    public Dictionary<Type, IState> states;
    public  Dictionary<Transform, float> bossPositions;


    // Start is called before the first frame update
    void Awake()
    {
        dialogue = GetComponent<ActivateDialogueFromBossAI>();
        lineRender = GetComponent<LineRenderer>();
        InitializeStateMachine();
        InitializeBossPositions();
       if(dialogueEnd != null)
        {
            dialogueEnd.OnDialogueEnd += HandleDialogueEnd;
        }
    }

    private void OnDestroy()
    {
        if(dialogueEnd != null)
        {
            dialogueEnd.OnDialogueEnd -= HandleDialogueEnd;
        }
    }

    private void HandleDialogueEnd()
    {
        endDialogueEvent();
    }

    public void ActivateDialogue()
    {
        dialogue.ActivateDialogue();
    }

    void InitializeBossPositions()
    {
        float angle = 90f;
        bossPositions = new Dictionary<Transform, float>();
        foreach(Transform location in bossLocations)
        {
            if (!bossPositions.ContainsKey(location))
            {
                bossPositions.Add(location, angle);
                angle += 90f;
            }
        }
    }

    void InitializeStateMachine()
    {
        states = new Dictionary<Type, IState>()
        {
            {typeof(FirstBossIdleState), new FirstBossIdleState(this) },
            {typeof(BossChargeState), new BossChargeState(this) },
            {typeof(BossCornerSpreadBulletPattern), new BossCornerSpreadBulletPattern(this) },
            {typeof(DashAndShootState), new DashAndShootState(this) }
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
        if(CurrentBossHealth < 1f && currentPhase < 4)
        {
            currentPhase++;
            //Death Phase
            states.Add(typeof(FirstBossDeathState), new FirstBossDeathState(this));
            StateMachine.SetStates(states, 0f);
            StateMachine.SwitchToNewState(typeof(FirstBossDeathState));
        }

        if(CurrentBossHealth < 25f && currentPhase < 3)
        {
            currentPhase++;
            //Add new initial phase to dictionary + intermediate attack
            //Set initial state as the new state 
            states.Add(typeof(FirstBossPhase3State), new FirstBossPhase3State(this));
            StateMachine.SetStates(states, 25f);
            StateMachine.SwitchToNewState(typeof(FirstBossPhase3State));
        }
        else if (CurrentBossHealth < 50f && currentPhase < 2)
        {
            currentPhase++;
            //Add new initial phase to dictionary + intermediate attack
            //Set initial state as the new state 
            states.Add(typeof(FirstBossPhase2State), new FirstBossPhase2State(this));
            StateMachine.SetStates(states, 25f);
            StateMachine.SwitchToNewState(typeof(FirstBossPhase2State));
        }
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
        GameObject enemyBullet = ObjectPooler.Instance.GetFromPool("Enemy Bullet 1");
        if(enemyBullet != null)
        {
            enemyBullet.transform.position = startPos;
            enemyBullet.transform.rotation = rotation;
        }
        return enemyBullet;
    }

    public GameObject CreateLandMine(Vector2 startPos)
    {
        GameObject mine = ObjectPooler.Instance.GetFromPool("Enemy Mine");
        if (mine != null)
        {
            mine.transform.position = startPos;
        }
        return mine;
    }

    public GameObject CreateDrone(Vector2 startPos)
    {
        GameObject drone = ObjectPooler.Instance.GetFromPool("Enemy Drone");
        if (drone != null)
        {
            drone.transform.position = startPos;
            var shoot = drone.GetComponent<ShootProjectileToPlayer>();
            if(startPos.x < 0)
            {
                SpriteRenderer render = drone.GetComponentInChildren<SpriteRenderer>();
                render.flipX = true;
            }
            if(shoot != null)
            {
                shoot.SetPlayer(player);
            }
        }
        return drone;
    }

    public void SetBossTrigger(string trigger)
    {
        if(animate != null)
        {
            animate.SetBossTrigger(trigger);
        }
    }

}