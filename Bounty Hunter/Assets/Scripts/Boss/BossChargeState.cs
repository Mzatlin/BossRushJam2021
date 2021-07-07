using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossChargeState : BossStateBase
{
    //values that adjust based on Phase
    float chargeSpeed = 30f;
    float chargeDelay = 3f;
    int chargeAmounts = 3;

    //values fetched from the master AI controller
    GameObject player;
    Transform startPosition;
    LayerMask obstacleLayers;
    LineRenderer render;

    float chargeDamage = 2f;
    bool isCharging = false;
    bool isAiming = false;
    bool isEnd = false;
    Vector2 moveDirection;


    FirstBossAI boss;
    public BossChargeState(FirstBossAI _boss) : base(_boss.gameObject)
    {
        boss = _boss;
    }

    public override void BeginState()
    {
        render = boss.GetLineRenderer();
        render.enabled = false;
        player = boss.GetPlayer();
        startPosition = boss.GetCenterPosition();
        boss.hitEvent += HandleHit;
        chargeAmounts = 1;
        isEnd = false;
        chargeDelay /= boss.currentPhase;
        ResetEnemy();
    }

    private void HandleHit(Collider2D collision)
    {
        var hit = collision.GetComponent<IHittablle>();
        if (hit != null && isCharging)
        {
            hit.ProcessDamage(chargeDamage);
        }
        isCharging = false;
        boss.HandleCoroutine(ResetDelay());
    }

    public override void EndState()
    {
        render.enabled = false;
    }

    public override Type Tick()
    {
        if (!isCharging)
        {
            if (render != null && isAiming)
            {
                render.enabled = true;
                render.SetPosition(0, (player.transform.position) / 4);
                render.SetPosition(1, boss.transform.position);
            }
            moveDirection = (player.transform.position - boss.transform.position).normalized;
        }
        else
        {
            render.enabled = false;
            boss.transform.position += (Vector3)(moveDirection * chargeSpeed * Time.deltaTime);
        }

        return GetEnd();

    }

    Type GetEnd()
    {
        if (isEnd)
        {
            render.enabled = false;
            return typeof(FirstBossIdleState);
        }
        else
        {
            return null;
        }
    }

    void ResetEnemy()
    {
        //boss.transform.position = startPosition.position;
        if (chargeAmounts > 0 && !isAiming)
        {
            isAiming = true;
            boss.HandleCoroutine(ChargeDelay());
            chargeAmounts--;
        }
        else
        {
            Debug.Log("End Attack");
            boss.hitEvent -= HandleHit;
            isEnd = true;
        }
    }

    IEnumerator ChargeDelay()
    {

        yield return new WaitForSeconds(chargeDelay);
        isCharging = true;
        isAiming = false;
    }

    IEnumerator ResetDelay()
    {
        yield return new WaitForSeconds(chargeDelay / 1.5f);
        ResetEnemy();
    }

}
