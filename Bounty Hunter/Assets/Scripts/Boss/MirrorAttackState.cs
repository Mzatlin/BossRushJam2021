using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MirrorAttackState : BossStateBase
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
    float nextFireTime = 0f;
    float fireDelay = .5f;
    Transform lastPosition;


    Vector3 startPoint;
    const float radius = 1F;

    Quaternion gunRotation;

    public MirrorAttackState(MagicianBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }

    public override void BeginState()
    {
        ResetState();
    }

    void ResetState()
    {
        projectileAmount = 10;
        projectileSpeed = 100f;
        fireRate = 2f;
        jumpAmount = baseJumpAmount + boss.currentPhase;
        isShooting = false;
        isJumping = false;
    }

    Transform GetNextPosition()
    {
        int randomPos = UnityEngine.Random.Range(0, boss.sideBossLocations.Length);
        Transform nextPos = boss.sideBossLocations[randomPos];
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
        ResetState();
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

            startPoint = GetNextPosition().position;
            boss.HandleCoroutine(TeleportTime(startPoint));
        }
    }

    void HandleFiring()
    {
        if (!isShooting && Time.time > nextFireTime)
        {
            isShooting = true;
            for (int i = 0; i < boss.currentPhase * 2; i++)
            {
                boss.FireRandomMirror();
            }
            nextFireTime = Time.time + fireDelay;

        }
    }


    IEnumerator TeleportTime(Vector2 endPos)
    {
        isJumping = true;
        boss.SetBossTrigger("WarpOut");
        yield return new WaitForSeconds(0.35f);
        boss.EnableBoss(false);
        yield return new WaitForSeconds(2f);
        boss.transform.position = endPos;
        boss.EnableBoss(true);
        boss.SetBossTrigger("WarpIn");
        yield return new WaitForSeconds(.35f);
        boss.SetBossTrigger("Attack");
        yield return new WaitForSeconds(.35f);
        boss.HandleCoroutine(SpawnProjectile(10));
        yield return new WaitForSeconds(2f);
        isJumping = false;
        jumpAmount--;


    }

    IEnumerator SpawnProjectile(int projectileAmount)
    {
        int randomNum = UnityEngine.Random.Range(0, boss.cannonPositions.Length);

        Vector2 projectileVector = boss.transform.position.x < 0 ? boss.transform.right : -boss.transform.right;
        for (int i = 0; i < projectileAmount; i++)
        {
           
            Vector2 projectileMoveDirection = (projectileVector - (Vector2)boss.transform.position).normalized;

            //Logic for determining how the bullet if fired
            GameObject tmpObj = boss.CreateBullet(boss.transform.position, Quaternion.identity);
            tmpObj.transform.rotation = boss.SetupBullet(tmpObj, projectileMoveDirection);
            yield return new WaitForSeconds(.2f);
        }

        isShooting = false;
    }

}
