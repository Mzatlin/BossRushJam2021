using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCornerSpreadBulletPattern : BossStateBase
{
    FirstBossAI boss;
    public int projectileAmount;
    public float projectileSpeed;
    public GameObject projectilePrefab;
    public float fireRate = 0.3f;
    float nextJumpTime = 0f;
    int jumpAmount = 0;
    int baseJumpAmount = 2;
    bool isShooting = false;
    bool isJumping = false;
    Quaternion bulletAngle;
    float angle = 90f;
    bool isAttacking = false;
    Transform lastPosition;


    Vector3 startPoint;
    const float radius = 1F;

    Quaternion gunRotation;

    public BossCornerSpreadBulletPattern(FirstBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }

    public override void BeginState()
    {
        projectileAmount = 10;
        projectileSpeed = 100f;
        fireRate = 2f;
        jumpAmount = baseJumpAmount + boss.currentPhase;
    }

    Transform GetNextPosition()
    {
        int randomPos = UnityEngine.Random.Range(0, boss.bossLocations.Length);
        Transform nextPos = boss.bossLocations[randomPos];
        if(lastPosition == null || nextPos != lastPosition)
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
 
        if(Time.time > nextJumpTime)
        {
            if(!isJumping && jumpAmount > 0 && isShooting == false)
            {
                nextJumpTime = Time.time + fireRate;
                startPoint = GetNextPosition().position;
                angle = boss.bossPositions[lastPosition];
                boss.HandleCoroutine(jumpTime(startPoint));
            }
            if (jumpAmount < 1)
            {
                return typeof(FirstBossIdleState);
            }
        }
        return null;
    }



    void SpawnProjectile(int projectileAmount)
    {
        float angleStep = 360f / projectileAmount;


        for (int i = 0; i < projectileAmount; i++)
        {
            //float projectileDirectionX = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            // float projectileDirectionY = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            float projectileDirectionX = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float projectileDirectionY = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180f);


            Vector2 projectileVector = new Vector2(projectileDirectionX, projectileDirectionY);
            Vector2 projectileMoveDirection = (projectileVector - (Vector2)startPoint).normalized * projectileSpeed;

            GameObject tmpObj = boss.CreateBullet(startPoint, Quaternion.identity); //Instantiate(projectilePrefab, startPoint, Quaternion.identity); 
            float bulletangle = Mathf.Atan2(projectileMoveDirection.y, projectileMoveDirection.x) * Mathf.Rad2Deg;

            gunRotation.eulerAngles = new Vector3(0, 0, bulletangle);
            tmpObj.transform.rotation = gunRotation;

            //angle += angleStep;
            angle += 10f;
        }
        isAttacking = false;
    }

    IEnumerator jumpTime(Vector2 endPos)
    {
        isJumping = true;
        isShooting = true;
        float lerpSpeed = 30f;
        Vector2 startPos = boss.transform.position;
        float totalDistance = Vector2.Distance(startPos, endPos);
        float fractionOfJourney = 0;
        float startTime = Time.time;

        while (fractionOfJourney < 1)
        {
            fractionOfJourney = ((Time.time - startTime) * lerpSpeed) / totalDistance;
            boss.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }

        isJumping = false;
        jumpAmount--;

        boss.HandleCoroutine(SpawnProjectile(projectileAmount, 0.1f));
    }

    IEnumerator SpawnProjectile(int projectileAmount, float delay)
    {
        float angleStep = 360f / projectileAmount;

        for (int i = 0; i < projectileAmount; i++)
        {
            float projectileDirectionX = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float projectileDirectionY = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180f);


            Vector2 projectileVector = new Vector2(projectileDirectionX, projectileDirectionY);
            Vector2 projectileMoveDirection = (projectileVector - (Vector2)startPoint).normalized * projectileSpeed;

            GameObject tmpObj = boss.CreateBullet(startPoint, Quaternion.identity); //Instantiate(projectilePrefab, startPoint, Quaternion.identity); 
            float bulletangle = Mathf.Atan2(projectileMoveDirection.y, projectileMoveDirection.x) * Mathf.Rad2Deg;

            gunRotation.eulerAngles = new Vector3(0, 0, bulletangle);
            tmpObj.transform.rotation = gunRotation;
            yield return new WaitForSeconds(delay);
            angle += 10f;
        }
        isShooting = false;
        yield return null;
    }

}
