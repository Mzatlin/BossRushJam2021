using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LaserSweepState : BossStateBase
{
    DJBossAI boss;
    float laserdelay = 4.5f;
    bool isDoneSpinning = false;
    float turnSpeed = 30f;
    bool hasStartedWait = false;
    bool canFire = false;

    public LaserSweepState(DJBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }

    public override void BeginState()
    {
        isDoneSpinning = false;
        hasStartedWait = false;
        turnSpeed = 35f;
        canFire = false;
    }

    public override void EndState()
    {
        boss.SetLasersActive(false);
    }

    public override Type Tick()
    {
        if (!isDoneSpinning)
        {
            if (!hasStartedWait)
            {
                hasStartedWait = true;
                Vector2 randomPos = boss.bossLocations[UnityEngine.Random.Range(0, boss.bossLocations.Length)].position;
                boss.HandleCoroutine(JumpTime(randomPos));
            }
            if (canFire)
            {
                SetupRayDirection(boss.lasers[0]);
            }

            return null;
        }
        else
        {
            boss.SetLasersActive(false);
            boss.transform.rotation = Quaternion.identity;
            return typeof(DJBossIdleState);
        }
    }

    protected override IEnumerator JumpTime(Vector2 endPos)
    {
        yield return base.JumpTime(endPos);
        yield return new WaitForSeconds(1f);
        canFire = true;
        boss.SetLasersActive(true);
        boss.HandleCoroutine(SpinDelay());
    }

    IEnumerator SpinDelay()
    {
        yield return new WaitForSeconds(laserdelay);
        isDoneSpinning = true;
    }

    void SetupRayDirection(LineRenderer render)
    {
        Vector2 direction = boss.transform.position.x > 0 ? -boss.transform.right : boss.transform.right;

        render.SetPosition(0, boss.transform.localPosition);
        Ray2D ray = new Ray2D(boss.transform.position, direction);
        RaycastHit2D hit;
        render.SetPosition(1, ray.direction);
        Debug.DrawRay(ray.origin, ray.direction);
        hit = Physics2D.Raycast(ray.origin, ray.direction, 1000f, boss.GetObstacleMask());
        if (hit.collider)
        {
            DamageTarget(hit, render);
        }
        else
        {
            Vector3 dir = boss.GetPlayer().transform.position - boss.transform.position;
            render.SetPosition(1, ray.direction * 10f);
        }
        Rotate();
    }

    void Rotate()
    {
        boss.transform.Rotate(0, 0, Time.deltaTime * turnSpeed);
    }

    void DamageTarget(RaycastHit2D hit, LineRenderer render)
    {
        var damage = hit.collider.GetComponent<IHittablle>();
        if (damage != null)
        {
            damage.ProcessDamage(1);
        }
        render.SetPosition(1, hit.point);
    }
}
