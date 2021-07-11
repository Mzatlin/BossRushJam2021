using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndFireLaser : MonoBehaviour
{
    LineRenderer lineRender;
    [SerializeField] LayerMask playerLayerMask;
    Quaternion laserRotation;
    [SerializeField] float turnSpeed = 10f;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        lineRender = GetComponentInChildren<LineRenderer>();
        lineRender.enabled = true;
        lineRender.SetPosition(0, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Ray2D ray = new Ray2D(transform.position, transform.right);
        RaycastHit2D hit;
        lineRender.SetPosition(1, ray.direction);
        hit = Physics2D.Raycast(ray.origin, ray.direction, 10f, playerLayerMask);
        //lineRender.SetPosition(1, transform.right * 3f);
        Debug.DrawRay(ray.origin, ray.direction);
        if (hit.collider)
        {
            var damage = hit.collider.GetComponent<IHittablle>();
            if (damage != null)
            {
                damage.ProcessDamage(1);
            }
            lineRender.SetPosition(1, hit.point);
        }
        else
        {
            Vector3 dir = player.transform.position - transform.position;
            lineRender.SetPosition(1, ray.direction * 10f);
        }
        transform.Rotate(0, 0, Time.deltaTime * turnSpeed);
    }
}
