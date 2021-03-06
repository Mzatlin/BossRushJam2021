using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpinState : BossStateBase
{
    DefenseSystemBossAI boss;
    float laserdelay = 4.5f;
    bool isDoneSpinning = false;
    float turnSpeed = 30f;
    bool hasStartedWait = false;
    bool isActive = false;

    public LaserSpinState(DefenseSystemBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }

    public override void BeginState()
    {
        isDoneSpinning = false;
        hasStartedWait = false;
        isActive = false;
        boss.SetLasers(false);
    }

    public override void EndState()
    {
        boss.SetLasers(false);
        boss.transform.rotation = Quaternion.identity;
        boss.SetBossTrigger("Idle");
    }

    public override Type Tick()
    {
        if(!isDoneSpinning)
        {
            if (!hasStartedWait)
            {
                hasStartedWait = true;
                boss.HandleCoroutine(ActiveDelay());
            }
            if (isActive)
            {
                SetupRayDirection(boss.transform.right, boss.lasers[0]);
                SetupRayDirection(-boss.transform.right, boss.lasers[1]);
                boss.HandleCoroutine(SpinDelay());
            }
            return null;
        }
        else
        {
            boss.SetLasers(false);
            boss.transform.rotation = Quaternion.identity;
            return typeof(DefenseSystemBossIdleState);
        }
    }
    IEnumerator SpinDelay()
    {
        yield return new WaitForSeconds(laserdelay);
        boss.SetBossTrigger("LaserCooldown");
        isDoneSpinning = true;
    }

    IEnumerator ActiveDelay()
    {
        boss.SetBossTrigger("LaserChargeUp");
        yield return new WaitForSeconds(1f);
        boss.SetLasers(true);
        boss.SetBossTrigger("LaserFire");
        isActive = true;
    }

    void SetupRayDirection(Vector2 direction, LineRenderer render)
    {
        render.SetPosition(0, boss.transform.localPosition);
        Ray2D ray = new Ray2D(boss.transform.position, direction);
        RaycastHit2D hit;
        render.SetPosition(1, ray.direction);
        hit = Physics2D.Raycast(ray.origin, ray.direction, 10f, boss.GetObstacleMask());
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
