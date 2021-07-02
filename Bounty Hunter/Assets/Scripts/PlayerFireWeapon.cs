using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireWeapon : MonoBehaviour
{
    float timeBeforeFire = 0f;

    [SerializeField] GameObject weapon;
    IShootable fire;
    IPlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<IPlayerStats>();
        if (weapon != null)
        {
            fire = weapon.GetComponent<IShootable>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && CanFire())
        {
            if (fire != null)
            {
                timeBeforeFire = Time.time + fire.FireRate;
                fire.FireWeapon();
            }
        }
    }

    bool CanFire()
    {
        return Time.time > timeBeforeFire && stats.GetPlayerReadiness();
    }
}
