using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBulletPattern : MonoBehaviour
{
    public int projectileAmount;
    public float projectileSpeed;
    public GameObject projectilePrefab;
    public float fireRate = 0.3f;
    float timeDelay = 0f;

    Vector3 startPoint;
    const float radius = 1F;

    Quaternion gunRotation;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeDelay)
        {
            startPoint = transform.position;
            timeDelay = Time.time + fireRate;
            SpawnProjectile(projectileAmount);
        }
    }

    void SpawnProjectile(int projectileAmount)
    {
        float angleStep = 360f / projectileAmount;
        float angle = 0f;

        for (int i = 0; i < projectileAmount; i++)
        {
            float projectileDirectionX = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirectionY = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2(projectileDirectionX, projectileDirectionY);
            Vector2 projectileMoveDirection = (projectileVector - (Vector2)startPoint).normalized * projectileSpeed;

            GameObject tmpObj = Instantiate(projectilePrefab, startPoint, Quaternion.identity);
            float bulletangle = Mathf.Atan2(projectileMoveDirection.y, projectileMoveDirection.x) * Mathf.Rad2Deg;

            gunRotation.eulerAngles = new Vector3(0, 0, bulletangle);
            tmpObj.transform.rotation = gunRotation;
            //tmpObj.GetComponent<Rigidbody2D>().velocity = new Vector3(projectileMoveDirection.x,projectileMoveDirection.y,0);

            angle += angleStep;

        }
    }
}
