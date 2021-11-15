using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FirstBossAI : BossAIBase
{
    public event Action<Collider2D> hitEvent = delegate { };
    public float currentPhaseThreshold = 50f;
   
    public Transform centerPoint;
    public Transform[] bossLocations;
    public Transform[] droneLocations;
    public Dictionary<Transform, float> bossPositions;

    [SerializeField] GameObject bullet;
    [SerializeField] GameObject landMine;
    [SerializeField] GameObject bossGun;
    [SerializeField] Transform bossFirePoint;

    Rigidbody2D rb;
    IGunRotate rotate => GetComponentInChildren<IGunRotate>();
    bool isDashing = false;
    LineRenderer lineRender;
    Vector2 imagePos = Vector2.zero;
    float distanceBetweenImages = 0.5f;

    // Awake is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        CheckBossDialogue();
        lineRender = GetComponent<LineRenderer>();
        InitializeBossPositions();
        if (dialogueEnd != null)
        {
            dialogueEnd.OnDialogueEnd += HandleDialogueEnd;
        }
        rb = GetComponent<Rigidbody2D>();
        bossAIAudio = FindObjectOfType<AudioManager>();
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
            {typeof(FirstBossIdleState), new FirstBossIdleState(this) },
            {typeof(BossChargeState), new BossChargeState(this) },
            {typeof(BossCornerSpreadBulletPattern), new BossCornerSpreadBulletPattern(this) },
            {typeof(DashAndShootState), new DashAndShootState(this) }
        };

        ResetStateMachineStates(states, 50);
    }

    void CheckBossDialogue()
    {
        if(bossDialogueCheck != null && bossDialogueCheck.isOpeningSet == false)
        {
            states.Add(typeof(FirstBossOpeningState), new FirstBossOpeningState(this));
            StateMachine.SetStates(states, 25f);
            ResetStateMachineStates(states, 50);
        }
    }

    public Rigidbody2D GetRigidBody()
    {
        return rb;
    }

    public LineRenderer GetLineRenderer()
    {
        return lineRender;
    }

    public Transform GetCenterPosition()
    {
        return centerPoint;
    }

    public bool GetOpeningStats()
    {
        return bossDialogueCheck.isOpeningSet;
    }

    public AudioManager GetAudioManager()
    {
        return bossAIAudio;
    }

    public void SetOpeningStats(bool isActive)
    {
        bossDialogueCheck.isOpeningSet = isActive;
    }   

    // Update is called once per frame
    void Update()
    {
        if(rb != null)
        {
            SetBossFloat("XMovement", rb.velocity.x);
            SetBossFloat("YMovement", rb.velocity.y);
        }

        CheckPhase();
        CheckDash();

    }
    void CheckDash()
    {
        if (isDashing && Vector2.Distance(transform.position, imagePos) > distanceBetweenImages)
        {
            GameObject instance = ObjectPooler.Instance.GetFromPool("FirstBossAfterImage");
            imagePos = transform.position;
        }
    }
    void CheckPhase()
    {
        if (CurrentBossHealth < 1f && currentPhase < 4)
        {
            currentPhase++;
            HandleStopCoroutine();
            //Death Phase
            states.Add(typeof(FirstBossDeathState), new FirstBossDeathState(this));
            StateMachine.SetStates(states, 0f);
            StateMachine.SwitchToNewState(typeof(FirstBossDeathState));
        }

        if (CurrentBossHealth < 65f && currentPhase < 3)
        {
            currentPhase++;
            HandleStopCoroutine();
            //Add new initial phase to dictionary + intermediate attack
            //Set initial state as the new state 
            states.Add(typeof(FirstBossPhase3State), new FirstBossPhase3State(this));
            StateMachine.SetStates(states, 25f);
            StateMachine.SwitchToNewState(typeof(FirstBossPhase3State));
        }
        else if (CurrentBossHealth < 100f && currentPhase < 2)
        {
            currentPhase++;
            HandleStopCoroutine();
            //Add new initial phase to dictionary + intermediate attack
            //Set initial state as the new state 
            states.Add(typeof(FirstBossPhase2State), new FirstBossPhase2State(this));
            StateMachine.SetStates(states, 25f);
            StateMachine.SwitchToNewState(typeof(FirstBossPhase2State));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & obstacleLayers) != 0)
        {
            hitEvent(collision);
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

    public void SetDash(bool setting)
    {
        isDashing = setting;
    }

}