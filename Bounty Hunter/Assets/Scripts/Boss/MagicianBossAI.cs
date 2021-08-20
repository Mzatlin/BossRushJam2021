using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MagicianBossAI : BossAIBase
{
    public event Action<Collider2D> hitEvent = delegate { };
    public float currentPhaseThreshold = 50f;
    public ParticleSystem explosionParticle;
    public Transform centerPoint;
    public Transform[] bossLocations;
    public Transform[] sideBossLocations;
    public Transform[] cannonPositions;
    public GameObject[] cannons;

    public List<GameObject> Mirrors = new List<GameObject>();

    [SerializeField] GameObject bullet;

    IBossAnimate animate => GetComponent<IBossAnimate>();
    IGunRotate rotate => GetComponentInChildren<IGunRotate>();

    LineRenderer lineRender;
    Collider2D bossCollider;
    SpriteRenderer sprite;

    // Awake is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        CheckBossDialogue();
        bossCollider = GetComponent<Collider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        lineRender = GetComponent<LineRenderer>();
        if (dialogueEnd != null)
        {
            dialogueEnd.OnDialogueEnd += HandleDialogueEnd;
        }

    }

    void CheckBossDialogue()
    {
        if (bossDialogueCheck != null && bossDialogueCheck.isOpeningSet == false)
        {
            states.Add(typeof(ThirdBossOpeningState), new ThirdBossOpeningState(this));
            StateMachine.SetStates(states, 25f);
            ResetStateMachineStates(states, 50);
        }
    }

    public bool GetOpeningStats()
    {
        return bossDialogueCheck.isOpeningSet;
    }
    public void SetOpeningStats(bool isActive)
    {
        bossDialogueCheck.isOpeningSet = isActive;
    }

    void Start()
    {
        SetHalfMirrorsActive(false);
    }

    public void ResetMirrors()
    {
        foreach (GameObject obj in Mirrors)
        {
            var move = obj.GetComponent<MoveObject>();
            if (move != null)
            {
                move.ResetMirror();
                move.SetMirrorStatus(true);
            }
        }
    }

    public void SetHalfMirrorsActive(bool isActive)
    {
        int i = 0;
        while (i < Mirrors.Count)
        {
            var move = Mirrors[i].GetComponent<MoveObject>();
            i += 2;
            if (move != null)
            {
                move.SetMirrorStatus(isActive);
            }
        }
    }

    public void FireAllMirrors()
    {
        foreach (GameObject obj in Mirrors)
        {
            var move = obj.GetComponent<FireMirrorBullet>();
            if (move != null)
            {
                move.LaunchMirrorBullet();
            }
        }
    }

    public void FireRandomMirror()
    {
        int index = UnityEngine.Random.Range(0, Mirrors.Count);
        if (!Mirrors[index].activeInHierarchy)
        {
            FireRandomMirror();
        }
        else
        {
            var move = Mirrors[index].GetComponent<FireMirrorBullet>();
            if (move != null)
            {
                move.LaunchMirrorBullet();
            }
        }

    }


    protected override void InitializeStateMachine()
    {
        states = new Dictionary<Type, IState>()
        {
            {typeof(MagicianIdleState), new MagicianIdleState(this) },
            {typeof(SneakAttackState), new SneakAttackState(this) },
            {typeof(DoubleMirrorBarrageState), new DoubleMirrorBarrageState(this)},
            {typeof(MirrorAttackState), new MirrorAttackState(this)},
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
            HandleStopCoroutine();
            //Death Phase
            states.Add(typeof(MagicianDeathPhase), new MagicianDeathPhase(this));
            StateMachine.SetStates(states, 0f);
            StateMachine.SwitchToNewState(typeof(MagicianDeathPhase));
        }

        if (CurrentBossHealth < 35f && currentPhase < 3)
        {
            currentPhase++;
            HandleStopCoroutine();
            //Add new initial phase to dictionary + intermediate attack
            //Set initial state as the new state 
            states.Add(typeof(MagicianBossPhase3), new MagicianBossPhase3(this));
            StateMachine.SetStates(states, 25f);
            StateMachine.SwitchToNewState(typeof(MagicianBossPhase3));
        }
        else if (CurrentBossHealth < 75f && currentPhase < 2)
        {
            currentPhase++;
            HandleStopCoroutine();
            //Add new initial phase to dictionary + intermediate attack
            //Set initial state as the new state 
            states.Add(typeof(MagicianBossPhase2State), new MagicianBossPhase2State(this));
            StateMachine.SetStates(states, 25f);
            StateMachine.SwitchToNewState(typeof(MagicianBossPhase2State));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & obstacleLayers) != 0)
        {
            hitEvent(collision);
        }
    }


    public void EnableBoss(bool isEnabled)
    {
        if (sprite != null && bossCollider != null && explosionParticle != null)
        {
            sprite.enabled = isEnabled;
            bossCollider.enabled = isEnabled;
            explosionParticle.Play();
        }
        else
        {
            Debug.Log("Sprite Renderer or Collider not found");
        }
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
        if (rotate != null)
        {
            rotate.AdjustLocalScale(bulletangle);
        }
        return gunRotation;
    }
}
