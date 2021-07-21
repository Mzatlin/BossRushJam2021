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
        bossCollider = GetComponent<Collider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        lineRender = GetComponent<LineRenderer>();
        if (dialogueEnd != null)
        {
            dialogueEnd.OnDialogueEnd += HandleDialogueEnd;
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
        var move = Mirrors[index].GetComponent<FireMirrorBullet>();
        if (move != null)
        {
            move.LaunchMirrorBullet();
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


    public void EnableBoss(bool isEnabled)
    {
        if(sprite != null && bossCollider != null && explosionParticle != null)
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
