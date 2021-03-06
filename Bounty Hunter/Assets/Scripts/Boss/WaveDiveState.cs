using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveDiveState : BossStateBase
{
    //values that adjust based on Phase
    float chargeSpeed = 10f;
    float chargeDelay = 1.5f;

    //values fetched from the master AI controller
    GameObject player;
    Transform startLocation;
    LayerMask obstacleLayers;

    float chargeDamage = 2f;
    bool isCharging = false;
    bool isWaiting = false;
    Vector2 moveDirection;

    float timeDelay = .2f;
    float timeThreshold = 0f;
    float jumpAmount = 2f;


    DJBossAI boss;
    public WaveDiveState(DJBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }

    public override void BeginState()
    {
        jumpAmount = 2f;
        player = boss.GetPlayer();
        startLocation = boss.firstBoss.transform;
        boss.hitEvent += HandleHit;
        chargeDelay /= boss.currentPhase;
        isCharging = false;
    }

    private void HandleHit(Collider2D collision)
    {
        var hit = collision.GetComponent<IHittablle>();
        if (hit != null && isCharging)
        {
            hit.ProcessDamage(chargeDamage);
        }
    }

    public override void EndState()
    {
        isCharging = false;
        boss.SetBossBool("IsWaveCharging", false);
    }

    public override Type Tick()
    {
        if (jumpAmount < 1)
        {
            return typeof(DJBossIdleState);
        }
        if (!isCharging)
        {
            moveDirection = (startLocation.position - boss.transform.position).normalized;
            if (!isWaiting)
            {
                boss.HandleCoroutine(ChargeDelay());
            }
        }
        else
        {
            Ray2D ray = new Ray2D(boss.transform.position, moveDirection);
            Debug.DrawRay(ray.origin, ray.direction);
            if (Time.time > timeThreshold)
            {
                timeThreshold = Time.time + timeDelay;
                SpawnProjectile(moveDirection.y, -moveDirection.x);
                SpawnProjectile(-moveDirection.y, moveDirection.x);
            }
            boss.transform.position += (Vector3)(moveDirection * chargeSpeed * Time.deltaTime);
        }

        return CheckBounds();

    }

    Type CheckBounds()
    {
        if (boss.transform.position.y < -6 || boss.transform.position.y > 5)
        {
            jumpAmount--;
            moveDirection *= -1;
            return typeof(DJBossIdleState);

        }
        if (boss.transform.position.x < -11 || boss.transform.position.x > 11)
        {
            jumpAmount--;
            moveDirection *= -1;
            return typeof(DJBossIdleState);
        }
        return null;
    }


    void SpawnProjectile(float x, float y)
    {
        Vector2 projectileVector = new Vector2(x, y);
       // Vector2 projectileMoveDirection = (projectileVector - (Vector2)boss.transform.position).normalized;
        GameObject tmpObj = boss.CreateBullet(boss.transform.position, Quaternion.identity);
        tmpObj.transform.rotation = boss.SetupBullet(tmpObj, projectileVector);
    }


    IEnumerator ChargeDelay()
    {
        boss.SetBossBool("IsWaveCharging", true);
        yield return new WaitForSeconds(chargeDelay);
        boss.SetBossBool("IsWaveCharging",false);
        isCharging = true;
    }
}
