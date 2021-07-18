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
                boss.HandleCoroutine(TeleportTime(new Vector2(UnityEngine.Random.Range(-7, 10), UnityEngine.Random.Range(-3, 6))));//GetRandomPosition()));
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
        boss.EnableBoss(false);
        yield return new WaitForSeconds(2f);
        boss.transform.position = endPos;

        isJumping = false;
        jumpAmount--;
        boss.EnableBoss(true);
        boss.HandleCoroutine(SpawnProjectile(10, 0.1f));
    }

    IEnumerator SpawnProjectile(int projectileAmount, float delay)
    {
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < projectileAmount; i++)
        {
            //Direction vector of bullet
            Vector2 direction = (boss.GetPlayer().transform.position - boss.transform.position).normalized;

            GameObject bullet = boss.CreateBullet(boss.transform.position, Quaternion.identity);
            bullet.transform.rotation = boss.SetupBullet(bullet, direction);

            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(1f);
        isShooting = false;
        yield return null;
    }
}
