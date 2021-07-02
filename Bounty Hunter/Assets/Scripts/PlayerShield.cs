using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    IPlayerStats stats;
    IHittablle hit;
    bool isShieldActive = false;
    // Start is called before the first frame update
    void Start()
    {
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

        if (stats != null && hit != null && stats.GetPlayerReadiness() && !isShieldActive && Input.GetMouseButton(1))
        {
            stats.SetPlayerReadiness(false);
            isShieldActive = true;
            hit.CanHit = false;
        }
        else if(isShieldActive && Input.GetMouseButtonUp(1))
        {
            stats.SetPlayerReadiness(true);
            isShieldActive = false;
            hit.CanHit = true;
        }

    }
}
