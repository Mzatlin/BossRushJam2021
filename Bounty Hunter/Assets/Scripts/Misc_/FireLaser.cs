using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaser : MonoBehaviour
{
    LineRenderer lineRender;
    [SerializeField] LayerMask playerLayerMask;
    Quaternion laserRotation;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    // Start is called before the first frame update
    void Start()
    {
        lineRender = GetComponent<LineRenderer>();
        lineRender.enabled = true;
        lineRender.SetPosition(0, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        lineRender.SetPosition(0, pointA.position);
        Ray2D ray = new Ray2D(transform.position, transform.right);
        RaycastHit2D hit;
        hit = Physics2D.Raycast(ray.origin, ray.direction, Vector2.Distance(pointA.position,pointB.position), playerLayerMask);
        //lineRender.SetPosition(1, transform.right*3f);
        lineRender.SetPosition(1, lineRender.GetPosition(0) * 10);
        if (hit.collider)
        {
            var damage = hit.collider.GetComponent<IHittablle>();
            if(damage != null)
            {
                damage.ProcessDamage(1);
            }
            lineRender.SetPosition(1, hit.point);
        }
         lineRender.SetPosition(1, pointB.position);


    }
}
