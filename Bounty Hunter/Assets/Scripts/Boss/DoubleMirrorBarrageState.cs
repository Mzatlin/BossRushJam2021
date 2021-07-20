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
    float fireDelay = .5f;
    Transform lastPosition;


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

        if (Time.time > nextJumpTime)
        {
            if (!isJumping && jumpAmount > 0)
            {
                HandleJumping();
               // HandleFiring();
            }
            if (jumpAmount < 1)
            {
                return typeof(MagicianIdleState);
            }
        }
        return null;
    }

    void HandleJumping()
    {
        nextJumpTime = Time.time + fireRate;
        angle = 0;

        startPoint = GetNextPosition().position;
        boss.HandleCoroutine(TeleportTime(startPoint));
    }

    void HandleFiring()
    {
        if(Time.time > nextFireTime)
        {
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

      //  boss.HandleCoroutine(SpawnProjectile(projectileAmount, 0.1f));
    }

    IEnumerator SpawnProjectile(int projectileAmount, float delay)
    {
        float angleStep = 360f / projectileAmount;

        for (int i = 0; i < projectileAmount; i++)
        {
            //Direction vector of bullet
            float projectileDirectionX = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float projectileDirectionY = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180f);
            Vector2 projectileVector = new Vector2(projectileDirectionX, projectileDirectionY);
            Vector2 projectileMoveDirection = (projectileVector - (Vector2)startPoint).normalized;

            //Logic for determining how the bullet if fired
            GameObject tmpObj = boss.CreateBullet(startPoint, Quaternion.identity);
            tmpObj.transform.rotation = boss.SetupBullet(tmpObj, projectileMoveDirection);
            yield return new WaitForSeconds(delay);

            angle += 10f;
        }
        isShooting = false;
        yield return null;
    }

}
