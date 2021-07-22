using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoubleMirrorBarrageState : BossStateBase
{
    MagicianBossAI boss;
    public int projectileAmount;
    public float projectileSpeed;
    public GameObject projectilePrefab;
    public float fireRate = 0.3f;
    float nextJumpTime = 0f;
    int jumpAmount = 0;
    int baseJumpAmount = 2;
    bool isShooting = false;
    bool isJumping = false;
    bool isAttacking = false;
    float angle = 90f;
    float nextFireTime = 0f;
    float fireDelay = 3.5f;
    Transform lastPosition;
    Animator animate;
    int randomNum = -1;
    bool isTriggered = false;

    Vector3 startPoint;
    const float radius = 1F;

    Quaternion gunRotation;

    public DoubleMirrorBarrageState(MagicianBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }

    public override void BeginState()
    {
        Debug.Log("Enter Barrage State");
        projectileAmount = 10;
        projectileSpeed = 100f;
        fireRate = 2f;
        jumpAmount = baseJumpAmount + boss.currentPhase;
        nextFireTime = Time.time + fireDelay;
    }

    Transform GetNextPosition()
    {
        int randomPos = UnityEngine.Random.Range(0, boss.bossLocations.Length);
        Transform nextPos = boss.bossLocations[randomPos];
        if (lastPosition == null || nextPos != lastPosition)
        {
            lastPosition = nextPos;
            return lastPosition;
        }
        else
        {
            return GetNextPosition();
        }
    }


    public override void EndState()
    {
        throw new NotImplementedException();
    }

    public override Type Tick()
    {
        if (jumpAmount < 1)
        {
            return typeof(MagicianIdleState);
        }
        HandleJumping();
        HandleFiring();

        return null;
    }

    void HandleJumping()
    {
        if (Time.time > nextJumpTime && !isJumping && jumpAmount > 0)
        {
            nextJumpTime = Time.time + fireRate;
            angle = 0;

            startPoint = GetNextPosition().position;
            boss.HandleCoroutine(TeleportTime(startPoint));
        }
    }

    void HandleFiring()
    {
        if (!isShooting && Time.time > nextFireTime)
        {
            isShooting = true;

            boss.HandleCoroutine(SpawnProjectile(15, randomNum));
            nextFireTime = Time.time + fireDelay;
        }
    }


    IEnumerator TeleportTime(Vector2 endPos)
    {
        isJumping = true;
        boss.EnableBoss(false);
        yield return new WaitForSeconds(2f);
        boss.transform.position = endPos;
        boss.EnableBoss(true);
        yield return new WaitForSeconds(2f);
        isJumping = false;
        jumpAmount--;


    }

    IEnumerator SpawnProjectile(int projectileAmount, int randomNum)
    {
        randomNum = UnityEngine.Random.Range(0, boss.cannonPositions.Length);
        animate = boss.cannons[randomNum].GetComponent<Animator>();
        if (animate != null && !isTriggered)
        {
            animate.SetTrigger("Loading");
            isTriggered = true;
        }
        yield return new WaitForSeconds(1f);
        if (animate != null)
        {
            animate.SetTrigger("Fired");
        }
        float angleStep = 180f / projectileAmount;
        if (randomNum <= (boss.cannonPositions.Length / 2) - 1)
        {
            angleStep *= -1;
        }

        angle = 0f;

        Vector2 CannonPoint = boss.cannonPositions[randomNum].position;

        for (int i = 0; i < projectileAmount; i++)
        {
            //Direction vector of bullet
            float projectileDirectionX = CannonPoint.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float projectileDirectionY = CannonPoint.y + Mathf.Cos((angle * Mathf.PI) / 180f);
            Vector2 projectileVector = new Vector2(projectileDirectionX, projectileDirectionY);
            Vector2 projectileMoveDirection = (projectileVector - CannonPoint).normalized;

            //Logic for determining how the bullet if fired
            GameObject tmpObj = boss.CreateBullet(CannonPoint, Quaternion.identity);
            tmpObj.transform.rotation = boss.SetupBullet(tmpObj, projectileMoveDirection);


            angle += angleStep;
        }
        isShooting = false;
        isTriggered = false;
    }

}
