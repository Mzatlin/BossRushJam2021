using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DJBossAI : BossAIBase
{
    public event Action<Collider2D> hitEvent = delegate { };
    public List<LineRenderer> lasers = new List<LineRenderer>();
    public Transform[] bossLocations;
    public Transform[] bossIdleLocations;
    public GameObject pivot;
    public GameObject firstBoss;
    public ParticleSystem particle;
    IStateMachine firstBossState;

    bool isUnpaused = false;
    protected override void InitializeStateMachine()
    {
        states = new Dictionary<Type, IState>()
        {
            { typeof(DJBossIdleState), new DJBossIdleState(this) },
             {typeof(WaveDiveState), new WaveDiveState(this) },
             {typeof(LaserSweepState), new LaserSweepState(this) }
        };

        ResetStateMachineStates(states, 50);
        SetupLineRenderers();
    }

    public void ActivateParticle(bool isActive, Vector2 direction)
    {
        if(particle != null)
        {
            var main = particle.main;
            main.simulationSpace = ParticleSystemSimulationSpace.Local;
            if (isActive)
            {
                float particleAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                gunRotation.eulerAngles = new Vector3(particleAngle, 90, particleAngle);
                particle.transform.rotation = gunRotation;
                particle.Play();
            }
            else
            {
                particle.Stop();
            }
        }

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

    protected override void SetLasersActive(bool toggle)
    {
        base.SetLasersActive(toggle);
        foreach (LineRenderer render in lasers)
        {
            render.enabled = toggle;
        }
    }

    public LayerMask GetObstacleMask()
    {
        return obstacleLayers;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (firstBoss != null)
        {
            firstBossState = firstBoss.GetComponent<IStateMachine>();
        }
        if (dialogueEnd != null)
        {
            dialogueEnd.OnDialogueEnd += HandleDialogueEnd;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentBossHealth < 50 && firstBossState != null && !isUnpaused)
        {
            isUnpaused = true;
            states.Add(typeof(DJBossPhase2State), new DJBossPhase2State(this));
            StateMachine.SetStates(states, 0f);
            StateMachine.SwitchToNewState(typeof(DJBossPhase2State));
        }

        if(CurrentBossHealth < 1)
        {
            states.Add(typeof(DJBossDeathState), new DJBossDeathState(this));
            StateMachine.SetStates(states, 0f);
            StateMachine.SwitchToNewState(typeof(DJBossDeathState));
        }
    }

    protected override void HandleDialogueEnd()
    {
        base.HandleDialogueEnd();
        if(CurrentBossHealth < 50 && isUnpaused)
        {
            firstBossState.PauseStateMachine();
            var phase = firstBoss.GetComponent<BossAIBase>();
            if(phase != null)
            {
                phase.SetBossTrigger("PowerOn");
                phase.SetPhase(2);
            }
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
        return gunRotation;
    }

    public void StartDJBoss()
    {
        states.Add(typeof(DJBossPhase2State), new DJBossPhase2State(this));
        StateMachine.SetStates(states, 0f);
        StateMachine.SwitchToNewState(typeof(DJBossPhase2State));
    }
}