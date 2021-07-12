using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpinState : BossStateBase
{
    DJBossAI boss;
    float laserdelay = 5f;
    bool isDoneSpinning = false;
    float turnSpeed = 30f;
    bool hasStartedWait = false;

    public LaserSpinState(DJBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }
    public override void BeginState()
    {
        Debug.Log("Entered Laser State");
        isDoneSpinning = false;
        hasStartedWait = false;
        SetLasersActive(true);
    }

    public override void EndState()
    {
        throw new NotImplementedException();
    }

    public override Type Tick()
    {
        if(!isDoneSpinning)
        {
            if (!hasStartedWait)
            {
                hasStartedWait = true;
                boss.HandleCoroutine(SpinDelay());
            }
            SetupRayDirection(boss.transform.right, boss.lasers[0]);
            SetupRayDirection(-boss.transform.right, boss.lasers[1]);
            return null;
        }
        else
        {
            SetLasersActive(false);
            return typeof(DJBossIdleState);
        }
    }
    IEnumerator SpinDelay()
    {
        yield return new WaitForSeconds(laserdelay);
        isDoneSpinning = true;
        Debug.Log("DoneSpinning set to true");
    }

    void SetLasersActive(bool toggle)
    {
        foreach(LineRenderer render in boss.lasers)
        {
            render.enabled = toggle;
        }
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
