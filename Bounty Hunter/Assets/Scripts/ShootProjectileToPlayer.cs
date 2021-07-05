using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectileToPlayer : MonoBehaviour
{
    public float fireRate = 1f;
    float timeThreshold;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject player;
    Quaternion bulletAngle;


    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeThreshold)
        {
            timeThreshold = Time.time + fireRate;
            FireWeapon();
        }
    }

    void FireWeapon()
    {

        if (projectile != null && player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            bulletAngle.eulerAngles = new Vector3(0, 0, angle);
            Instantiate(projectile, transform.position, bulletAngle);
        }
    }

    public void SetPlayer(GameObject obj)
    {
        player = obj;
    }
}
