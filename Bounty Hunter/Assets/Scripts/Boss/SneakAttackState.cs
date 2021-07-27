using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SneakAttackState : BossStateBase
{
    MagicianBossAI boss;
    float fireAmount = 10f;
    float baseFireAmount = 10f;
    float jumpAmount = 0f;
    float baseJumpAmount = 2f;
    float jumpdelay = 1.5f;
    float nextJumpTime = 0f;
    bool isJumping = false;
    bool isShooting = false;
    float angle = 0f;
    Quaternion bulletAngle;
    Vector2 lastPosition;
    List<Vector2> possiblePositions = new List<Vector2> { new Vector2(-5, 0), new Vector2(5, 0), new Vector2(0, 3) };
    // List<Vector2> possiblePositions = new List<Vector2> { new Vector2(-8, 6), new Vector2(-8, -4), new Vector2(10, 6), new Vector2(10, -4) };
    public SneakAttackState(MagicianBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }

    public override void BeginState()
    {
        fireAmount += baseFireAmount + boss.currentPhase;
        jumpAmount = baseJumpAmount + boss.currentPhase;
        Debug.Log("Sneak Attack Start");
    }

    public override void EndState()
    {
    }

    public override Type Tick()
    {
        if (Time.time > nextJumpTime)
        {
            if (!isJumping && jumpAmount > 0 && isShooting == false)
            {
                nextJumpTime = Time.time + jumpdelay;
                boss.HandleCoroutine(TeleportTime(new Vector2(UnityEngine.Random.Range(-7, 7), UnityEngine.Random.Range(-3, 3.5f))));//GetRandomPosition()));
            }
            if (jumpAmount < 1)
            {
                return typeof(MagicianIdleState);
            }
        }

        return null;
    }

    Vector2 GetRandomPosition()
    {
        int randomPos = UnityEngine.Random.Range(0, possiblePositions.Count);
        Vector2 nextPos = possiblePositions[randomPos];
        if (lastPosition == null || nextPos != lastPosition)
        {
            lastPosition = nextPos;
            return lastPosition;
        }
        else
        {
            return GetRandomPosition();
        }
    }

    IEnumerator TeleportTime(Vector2 endPos)
    {
        isJumping = true;
        isShooting = true;
        boss.SetBossTrigger("WarpOut");
        yield return new WaitForSeconds(0.35f);
        boss.EnableBoss(false);
        yield return new WaitForSeconds(1f);
        boss.transform.position = endPos;
        boss.EnableBoss(true);
        boss.SetBossTrigger("WarpIn");
        yield return new WaitForSeconds(0.35f);
        boss.HandleCoroutine(SpawnProjectile(20, 0.1f));
    }

    IEnumerator SpawnProjectile(int projectileAmount, float delay)
    {
        float angleStep = 360f / projectileAmount;
        int randomNum = UnityEngine.Random.Range(0, 2);
        angle = 0;

        yield return new WaitForSeconds(1f);
        boss.SetBossTrigger("Attack");
        yield return new WaitForSeconds(.35f);
        for (int i = 0; i < projectileAmount; i++)
        {
            //Direction vector of bullet
            Vector2 direction = GetRandomDirection(angleStep, randomNum);

            GameObject bullet = boss.CreateBullet(boss.transform.position, Quaternion.identity);
            bullet.transform.rotation = boss.SetupBullet(bullet, direction);

            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(.5f);
        isShooting = false;
        isJumping = false;
        jumpAmount--;
        yield return null;
    }

    Vector2 GetRandomDirection(float angleStep, int randomNum)
    {
        Vector2 direction = Vector2.zero;
      
        if (randomNum < 1)
        {
            direction = (boss.GetPlayer().transform.position - boss.transform.position).normalized;
        }
        else
        {
            float projectileDirectionX = boss.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float projectileDirectionY = boss.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);
            Vector2 projectileVector = new Vector2(projectileDirectionX, projectileDirectionY);
            direction = (projectileVector - (Vector2)boss.transform.position).normalized;
            angle += angleStep;
        }

        return direction;
    }
}
