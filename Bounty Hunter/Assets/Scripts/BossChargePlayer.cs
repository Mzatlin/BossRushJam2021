using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChargePlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float chargeSpeed = 30f;
    [SerializeField] float chargeDelay = 3f;
    [SerializeField] Transform startPosition;
    [SerializeField] LayerMask obstacleLayers;
    [SerializeField] float chargeDamage = 2f;
    [SerializeField] LineRenderer render;
    bool isCharging = false;
    bool isAiming = false;
    Rigidbody2D rb;
    int chargeAmounts = 3;
    Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetEnemy();
        render = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isCharging)
        {
            if (render != null && isAiming)
            {
                render.enabled = true;
                render.SetPosition(0, (player.transform.position)/2 );
                render.SetPosition(1, transform.position);
            }
            moveDirection = (player.transform.position - transform.position).normalized;
        }
        else
        {
            render.enabled = false;
            transform.position += (Vector3)(moveDirection * chargeSpeed * Time.fixedDeltaTime);
        }


        //rb.velocity = moveDirection * chargeSpeed * Time.fixedDeltaTime;
    }

    void ResetEnemy()
    {
        if (chargeAmounts > 0)
        {
            isAiming = true;
            StartCoroutine(ChargeDelay());
            chargeAmounts--;
        }
        else
        {
            Debug.Log("End Attack");
        }
        transform.position = startPosition.position;
    }

    IEnumerator ChargeDelay()
    {

        yield return new WaitForSeconds(chargeDelay);
        isCharging = true;
        isAiming = false;
    }

    IEnumerator ResetDelay()
    {
        yield return new WaitForSeconds(chargeDelay / 2);
        ResetEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & obstacleLayers) != 0)
        {
            var hit = collision.GetComponent<IHittablle>();
            if (hit != null && isCharging)
            {
                hit.ProcessDamage(chargeDamage);
            }
            isCharging = false;
            StartCoroutine(ResetDelay());
        }
    }

}
