using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectileToPlayer : MonoBehaviour, IShootable
{
    [SerializeField] float fireRate = 1f;
    [SerializeField] GameObject projectile;
    float timeThreshold;
    GameObject player;
    Quaternion bulletAngle;
    Animator animate;

    public float FireRate => fireRate;

    void Awake()
    {
        animate = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeThreshold)
        {
            timeThreshold = Time.time + fireRate;
            WeaponChargeUp();
            FireWeapon();
        }
    }

    void WeaponChargeUp()
    {
        if (animate)
        {
            animate.SetTrigger("Shoot");
        }
    }

   public void FireWeapon()
    {
        if (projectile != null && player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bulletAngle.eulerAngles = new Vector3(0, 0, angle);
            GameObject tempBullet = ObjectPooler.Instance.GetFromPool("Enemy Bullet 1");
            if(tempBullet != null)
            {
                tempBullet.transform.position = transform.position;
                tempBullet.transform.rotation = bulletAngle;
                var projectile = tempBullet.GetComponent<Projectile>();
                if (projectile != null)
                {
                    Debug.Log("Player");
                    projectile.SetBulletDirection(direction);
                }
            }
        }
    }


    public void SetPlayer(GameObject obj)
    {
        player = obj;
    }
}
