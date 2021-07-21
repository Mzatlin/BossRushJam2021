using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMirrorBullet : MonoBehaviour
{
    Quaternion gunRotation;
    Animator animate;
    private void Start()
    {
        animate = GetComponentInChildren<Animator>();
    }
    public void LaunchMirrorBullet()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        if (animate != null)
        {
            animate.SetTrigger("Charge");
        }
        yield return new WaitForSeconds(0.5f);
        float offset = -1f;
        for (int i = 0; i <= 4; i++)
        {
            GameObject bullet = CreateBullet((Vector2)transform.position + new Vector2(offset, 0), Quaternion.identity);
            Vector2 direction = transform.position.y < 0 ? transform.right : -transform.right;
            bullet.transform.rotation = SetupBullet(bullet, direction);
            offset += 0.5f;
        }
    }

    Quaternion SetupBullet(GameObject bullet, Vector2 direction)
    {
        float bulletangle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gunRotation.eulerAngles = new Vector3(0, 0, bulletangle);
        var projectile = bullet.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.SetBulletDirection(direction);
        }
        return gunRotation;
    }

    GameObject CreateBullet(Vector3 startPos, Quaternion rotation)
    {
        GameObject enemyBullet = ObjectPooler.Instance.GetFromPool("Mirror");
        if (enemyBullet != null)
        {
            enemyBullet.transform.position = startPos;
            enemyBullet.transform.rotation = rotation;
        }
        return enemyBullet;

    }
}
