using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BossAIBase : MonoBehaviour
{
    public event Action endDialogueEvent = delegate { };
    public BossMechanicStateMachine StateMachine => GetComponent<BossMechanicStateMachine>();
    public Dictionary<Type, IState> states;
    ActivateDialogueFromBossAI dialogue;
    protected IHealth health => GetComponent<IHealth>();
    protected IDialogueEnd dialogueEnd => GetComponent<IDialogueEnd>();
    BossDialogueManager bossDialogue => GetComponent<BossDialogueManager>();
    IBossAnimate animate => GetComponent<IBossAnimate>();
    public float CurrentBossHealth => health.CurrentHealth;
    [SerializeField] protected GameObject player;
    [SerializeField] protected LayerMask obstacleLayers;
    [SerializeField] protected CurrentDaySO day;
    [SerializeField] protected BossDialogueSO bossDialogueCheck;
    protected Quaternion gunRotation;
    public int currentPhase = 1;
    protected Coroutine enemyCoroutine;


    protected virtual void Awake()
    {
        dialogue = GetComponent<ActivateDialogueFromBossAI>();
        InitializeStateMachine();
    }

    public void IncrementCurrentDay()
    {
        if (day != null)
        {
            day.IncrementDayCount();
        }
    }

    protected void OnDestroy()
    {
        if (dialogueEnd != null)
        {
            dialogueEnd.OnDialogueEnd -= HandleDialogueEnd;
        }
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

    public void SetBossTrigger(string trigger)
    {
        if (animate != null)
        {
            animate.SetBossTrigger(trigger);
        }
    }
    public void SetBossFloat(string param, float amount)
    {
        if (animate != null)
        {
            animate.SetBossFloat(param, amount);
        }
    }
    public void SetBossBool(string param, bool trigger)
    {
        if (animate != null)
        {
            animate.SetBossBool(param, trigger);
        }
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
        if (bossDialogue != null)
        {
            bossDialogue.SetNextDialogue();
        }
        dialogue.ActivateDialogue();
    }

    protected virtual void HandleDialogueEnd()
    {
        endDialogueEvent();
    }

    public GameObject CreateBullet(Vector3 startPos, Quaternion rotation)
    {
        GameObject enemyBullet = ObjectPooler.Instance.GetFromPool("Enemy Bullet 1");
        if (enemyBullet != null)
        {
            enemyBullet.transform.position = startPos;
            enemyBullet.transform.rotation = rotation;
        }
        return enemyBullet;
    }

    public void SetPhase(int phaseNum)
    {
        currentPhase = phaseNum;
    }

    protected void ClearBullets(string tag)
    {
        ObjectPooler.Instance.ClearPool(tag);
    }

    protected virtual void SetLasersActive(bool isActive){}

    public void SetLasers(bool toggle)
    {
        SetLasersActive(toggle);
    }
}
