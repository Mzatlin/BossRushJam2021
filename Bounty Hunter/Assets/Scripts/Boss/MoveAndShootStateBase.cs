using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveAndShootStateBase : BossStateBase
{
    FirstBossAI boss;
    float fireAmount = 10f;
    float baseFireAmount = 10f;
    float jumpAmount = 2f;
    int baseJumpAmount = 2;
    float jumpdelay = 3f;
    float nextJumpTime = 0f;
    bool isJumping = false;
    bool isShooting = false;
    Quaternion bulletAngle;
    Vector2 lastPosition;
    List<Vector2> possiblePositions = new List<Vector2> { new Vector2(-5, 0), new Vector2(5, 0), new Vector2(0, 3) };

    public MoveAndShootStateBase(FirstBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }

    public override void BeginState()
    {
        fireAmount = baseFireAmount + boss.currentPhase;
        jumpAmount = baseJumpAmount + boss.currentPhase;
    }

    public override void EndState()
    {
    }

    public override Type Tick()
    {
        if (Time.time > nextJumpTime)
        {
            if(!isJumping && jumpAmount > 0 && isShooting == false)
            {
                nextJumpTime = Time.time + jumpdelay;
                boss.HandleCoroutine(jumpTime(GetRandomPosition()));
            }
            if (jumpAmount < 1)
            {
                return typeof(FirstBossIdleState);
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

        boss.HandleCoroutine(SpawnProjectile(10, 0.1f));
    }

    IEnumerator SpawnProjectile(int projectileAmount, float delay)
    {
        for (int i = 0; i < projectileAmount; i++)
        {

            Vector2 direction = (boss.GetPlayer().transform.position - boss.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            bulletAngle.eulerAngles = new Vector3(0, 0, angle);
            GameObject bullet = boss.CreateBullet(boss.transform.position, Quaternion.identity);
            bullet.transform.rotation = bulletAngle;
            yield return new WaitForSeconds(delay);
        }
        isShooting = false;
        yield return null;
    }
}
