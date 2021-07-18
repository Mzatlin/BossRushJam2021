using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MagicianBossAI : BossAIBase
{
    public event Action<Collider2D> hitEvent = delegate { };
    public float currentPhaseThreshold = 50f;

    public Transform centerPoint;
    public Transform[] bossLocations;
    public Dictionary<Transform, float> bossPositions;

    public List<GameObject> Mirrors = new List<GameObject>();


    [SerializeField] GameObject bullet;
    [SerializeField] GameObject landMine;
    [SerializeField] GameObject bossGun;
    [SerializeField] Transform bossFirePoint;

    IBossAnimate animate => GetComponent<IBossAnimate>();
    IGunRotate rotate => GetComponentInChildren<IGunRotate>();

    LineRenderer lineRender;

    // Awake is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        CloseMirrors(new Vector2(5,0),2f);
        lineRender = GetComponent<LineRenderer>();
        InitializeBossPositions();
        if (dialogueEnd != null)
        {
            dialogueEnd.OnDialogueEnd += HandleDialogueEnd;
        }
    }

    public void CloseMirrors(Vector2 offset, float speed)
    {
        foreach(GameObject obj in Mirrors)
        {
            var move = obj.GetComponent<MoveObject>();
            if(move != null)
            {
                move.MoveToPosition(offset, speed);
            }
        }
    }


    void InitializeBossPositions()
    {
        float angle = 90f;
        bossPositions = new Dictionary<Transform, float>();
        foreach (Transform location in bossLocations)
        {
            if (!bossPositions.ContainsKey(location))
            {
                bossPositions.Add(location, angle);
                angle += 90f;
            }
        }
    }

    protected override void InitializeStateMachine()
    {
        states = new Dictionary<Type, IState>()
        {
            {typeof(MagicianIdleState), new MagicianIdleState(this) },
        };

        ResetStateMachineStates(states, 50);
    }


    public LineRenderer GetLineRenderer()
    {
        return lineRender;
    }

    public Transform GetCenterPosition()
    {
        return centerPoint;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPhase();
    }

    void CheckPhase()
    {
        if (CurrentBossHealth < 1f && currentPhase < 4)
        {
            currentPhase++;
            //Death Phase
          //  states.Add(typeof(FirstBossDeathState), new FirstBossDeathState(this));
          //  StateMachine.SetStates(states, 0f);
          //  StateMachine.SwitchToNewState(typeof(FirstBossDeathState));
        }

        if (CurrentBossHealth < 25f && currentPhase < 3)
        {
            currentPhase++;
            //Add new initial phase to dictionary + intermediate attack
            //Set initial state as the new state 
          //  states.Add(typeof(FirstBossPhase3State), new FirstBossPhase3State(this));
          //  StateMachine.SetStates(states, 25f);
          //  StateMachine.SwitchToNewState(typeof(FirstBossPhase3State));
        }
        else if (CurrentBossHealth < 50f && currentPhase < 2)
        {
            currentPhase++;
            //Add new initial phase to dictionary + intermediate attack
            //Set initial state as the new state 
           // states.Add(typeof(FirstBossPhase2State), new FirstBossPhase2State(this));
           // StateMachine.SetStates(states, 25f);
           // StateMachine.SwitchToNewState(typeof(FirstBossPhase2State));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & obstacleLayers) != 0)
        {
            hitEvent(collision);
        }
    }

    public void SetBossTrigger(string trigger)
    {
        if (animate != null)
        {
            animate.SetBossTrigger(trigger);
        }
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
            if (startPos.x < 0)
            {
                SpriteRenderer render = drone.GetComponentInChildren<SpriteRenderer>();
                render.flipX = true;
            }
            if (shoot != null)
            {
                shoot.SetPlayer(player);
            }
        }
        return drone;
    }

    public Quaternion SetupBullet(GameObject bullet, Vector2 direction)
    {
        float bulletangle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gunRotation.eulerAngles = new Vector3(0, 0, bulletangle);
        var projectile = bullet.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.SetBulletDirection(direction);
        }
        bossGun.transform.rotation = gunRotation;
        if (rotate != null)
        {
            rotate.AdjustLocalScale(bulletangle);
        }
        return gunRotation;
    }

    public void SetGunVisibility(bool visibility)
    {
        if (bossGun != null)
        {
            bossGun.SetActive(visibility);
        }
    }
}
