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
        //Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        GameObject bulletToSpawn = ObjectPooler.Instance.GetFromPool("Player Bullet");
        if(bulletToSpawn != null)
        {
            bulletToSpawn.transform.position = firePoint.transform.position;
            bulletToSpawn.transform.rotation = firePoint.transform.rotation;
            var projectile = bulletToSpawn.GetComponent<Projectile>();
            if(projectile != null)
            {
                projectile.SetBulletDirection(firePoint.transform.right);
            }

            bulletToSpawn.SetActive(true);
        }

    }

}
