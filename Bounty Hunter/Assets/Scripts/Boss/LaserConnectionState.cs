using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LaserConnectionState : BossStateBase
{
    DefenseSystemBossAI boss;
    float laserdelay = 2f;
    bool isLasersSet = false;
    bool hasStartedWait = false;
    int laserAmount = 2;
    int index = 0;
    int midpoint = 0;


    public LaserConnectionState(DefenseSystemBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }

    public override void BeginState()
    {
        isLasersSet = false;
        hasStartedWait = false;
        SetLasersActive(true);
        laserAmount = boss.laserPosts.Count / 2;
        midpoint = laserAmount;
        index = 0;
    }

    public override void EndState()
    {
        SetLasersActive(false);
        index = 0;
        laserAmount = boss.laserPosts.Count / 2;
    }

    public override Type Tick()
    {
        if (!isLasersSet)
        {
            if (!hasStartedWait && laserAmount > 0)
            {
                hasStartedWait = true;
                boss.HandleCoroutine(ChargeUpDelay());
            }
            //SetupRayDirection(boss.transform.right, boss.lasers[0]);
            return null;
        }
        else
        {
            SetLasersActive(false);
            return typeof(DefenseSystemBossIdleState);
        }
    }
    IEnumerator ChargeUpDelay()
    {
        yield return new WaitForSeconds(laserdelay);
        if (index < laserAmount)
        {
            ActivateLaserConnection(boss.segmentedLasers[index]);
        }
        isLasersSet = true;
    }

    IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(laserdelay);
        laserAmount--;
        hasStartedWait = false;
    }

    void ActivateLaserConnection(GameObject laser)
    {
        LineRenderer laserSegment = GetLineRenderer(laser);
        if (midpoint + index <= boss.laserPosts.Count - 1)
        {
            laserSegment.SetPosition(0, boss.laserPosts[index].transform.position);
            laserSegment.SetPosition(1, boss.laserPosts[midpoint+index].transform.position);
            laserSegment.enabled = true;
        }

        boss.HandleCoroutine(FireDelay());


    }

    void SetLasersActive(bool toggle)
    {
        foreach (GameObject render in boss.segmentedLasers)
        {
            var laser = render.GetComponent<LineRenderer>();
            if (laser != null)
            {
                laser.enabled = toggle;
            }

        }
    }

    LineRenderer GetLineRenderer(GameObject render)
    {
        var result = render.GetComponent<LineRenderer>();
        if (result != null)
        {
            return result;
        }
        else
        {
            return null;
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

