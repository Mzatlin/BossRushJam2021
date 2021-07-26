using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    IPlayerStats stats;
    IHittablle hit;
    Animator anim;
    bool isShieldActive = false;
    [SerializeField] float protectionAmount = 2f;
    [SerializeField] float timeBeforeShield = 0.4f;
    float timeThreshold = 0f;
    Coroutine shieldDelay;
    


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        hit = GetComponent<IHittablle>();
        stats = GetComponent<IPlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isShieldActive)
        {
            hit.CanHit = false;
        }

        if (stats != null && hit != null && stats.GetPlayerReadiness() && !isShieldActive && Input.GetMouseButtonDown(1))
        {
            if(timeThreshold < Time.time)
            {
                shieldDelay = StartCoroutine(ShieldUpTime());
            }


        }
        else if(isShieldActive && Input.GetMouseButtonUp(1))
        {
            if (shieldDelay != null)
            {
                StopCoroutine(shieldDelay);
            }

            DisableShield();
        }
    }

    IEnumerator ShieldUpTime()
    {
        EnableShield();
        yield return new WaitForSeconds(protectionAmount);
        DisableShield();
    }

    void EnableShield()
    {
        stats.SetPlayerReadiness(false);
        isShieldActive = true;
        hit.CanHit = false;
        anim.SetTrigger("Shield");
    }

    void DisableShield()
    {
        stats.SetPlayerReadiness(true);
        isShieldActive = false;
        hit.CanHit = true;
        anim.SetTrigger("Shield");
        timeThreshold = Time.time + timeBeforeShield;
    }
}
