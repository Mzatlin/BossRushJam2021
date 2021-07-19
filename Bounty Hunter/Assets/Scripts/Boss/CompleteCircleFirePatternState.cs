using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CompleteCircleFirePatternState : BossStateBase
{
    DefenseSystemBossAI boss;
    public int projectileAmount;
    public float projectileSpeed;
    public GameObject projectilePrefab;
    public float fireRate = 0.3f;
    float nextFireTime = 0f;
    int fireAmount = 0;
    int baseJumpAmount = 2;
    bool isShooting = false;
    bool isFiring = false;
    bool isAttacking = false;
    float angle = 90f;
    Transform lastPosition;


    Vector3 startPoint;
    const float radius = 1F;

    Quaternion gunRotation;

    public CompleteCircleFirePatternState(DefenseSystemBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }

    public override void BeginState()
    {
        projectileAmount = 50;
        projectileSpeed = 100f;
        fireRate = 2f/boss.currentPhase+.3f;
        fireAmount = baseJumpAmount + boss.currentPhase;
    }



    public override void EndState()
    {
    }

    public override Type Tick()
    {

        if (Time.time > nextFireTime)
        {
            if (fireAmount > 0 && !isShooting)
            {
                fireAmount--;
                nextFireTime = Time.time + fireRate;
                startPoint = boss.transform.position;
                boss.HandleCoroutine(SpawnProjectile(projectileAmount, 0.1f));
            }
            if (fireAmount < 1)
            {
                return typeof(DefenseSystemBossIdleState);
            }
        }
        return null;
    }

    IEnumerator SpawnProjectile(int projectileAmount, float delay)
    {
        float angleStep = 360f / projectileAmount;
        boss.SetBossTrigger("BulletCharge");
        yield return new WaitForSeconds(0.5f);
        boss.SetBossTrigger("BulletCool");
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
           // yield return new WaitForSeconds(delay);

            angle += 10f;
        }
        isShooting = false;
        yield return null;

    }

}
