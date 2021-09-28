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
    AudioManager audio;

    public float FireRate => fireRate;
    float chargupTime;
    float chargeThreshold = 0;
    bool isCharging;

    void Awake()
    {
        animate = GetComponentInChildren<Animator>();
        audio = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeThreshold && !isCharging)
        {
            isCharging = true;
            timeThreshold = Time.time + fireRate;
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
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        WeaponChargeUp();
        yield return new WaitForSeconds(0.45f);
        if (projectile != null && player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bulletAngle.eulerAngles = new Vector3(0, 0, angle);
            GameObject tempBullet = ObjectPooler.Instance.GetFromPool("Enemy Bullet 1");
            if (tempBullet != null)
            {
                audio.PlayAudioByString("Play_SingleAttack", tempBullet);
                tempBullet.transform.position = transform.position;
                tempBullet.transform.rotation = bulletAngle;
                var projectile = tempBullet.GetComponent<Projectile>();
                if (projectile != null)
                {
                    projectile.SetBulletDirection(direction);
                }
            }
        }
        isCharging = false;
        yield return null;
    }

    public void SetPlayer(GameObject obj)
    {
        player = obj;
    }
}
