using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticWeaponController : MonoBehaviour, IShootable
{
    [SerializeField] float fireRate;
    public float FireRate => fireRate;
    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject bullet;

    public void FireWeapon()
    {
        Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
    }

}
