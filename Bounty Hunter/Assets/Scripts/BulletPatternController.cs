using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPatternController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    Vector2 GetCircularDirection(Vector2 startPoint, float projectileSpeed, float angle)
    {
        float projectileDirectionX = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float projectileDirectionY = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180f);


        Vector2 projectileVector = new Vector2(projectileDirectionX, projectileDirectionY);
        Vector2 projectileMoveDirection = (projectileVector - (Vector2)startPoint).normalized * projectileSpeed;
        return projectileMoveDirection;

    }

    IEnumerator SpawnProjectile(int projectileAmount, float delay)
    {
        for(int i = 0; i < projectileAmount; i++)
        {


            yield return new WaitForSeconds(delay);
        }

    }
}
