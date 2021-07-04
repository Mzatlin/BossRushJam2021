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
    float timeDelay = 0f;
    int attackCount = 2;
    float angle = 90f;
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
        fireRate = 3f;
        attackCount = 3;//attackCount += boss.currentPhase;
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
        if (attackCount < 1)
        {
            //boss.transform.position = boss.centerPoint.position;
            return typeof(FirstBossIdleState);
        }
 
        else if(Time.time > timeDelay)
        {
            timeDelay = Time.time + fireRate;
            boss.transform.position = GetNextPosition().position;
            startPoint = bossGameObject.transform.position;
            angle = boss.bossPositions[lastPosition];
            
            SpawnProjectile(projectileAmount);
            attackCount--;
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
    }

}
