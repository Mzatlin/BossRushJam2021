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

    [SerializeField] List<LineRenderer> renders = new List<LineRenderer>();

    // Start is called before the first frame update
    void Start()
    {
        lineRender = GetComponentInChildren<LineRenderer>();
        lineRender.enabled = true;
        lineRender.SetPosition(0, transform.position);
        SetupLineRenderers();
    }

    void SetupLineRenderers()
    {
        foreach(Transform obj in transform)
        {
            var render = obj.gameObject.GetComponent<LineRenderer>();
            if(render != null)
            {
                renders.Add(render);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetupRayDirection(transform.right, renders[0]);
        SetupRayDirection(-transform.right, renders[1]);
    }


    void SetupRayDirection(Vector2 direction, LineRenderer render)
    {
        render.SetPosition(0, transform.position);
        Ray2D ray = new Ray2D(transform.position, direction);
        RaycastHit2D hit;
        render.SetPosition(1, ray.direction);
        hit = Physics2D.Raycast(ray.origin, ray.direction, 10f, playerLayerMask);
        if (hit.collider)
        {
            DamageTarget(hit, render);
        }
        else
        {
            Vector3 dir = player.transform.position - transform.position;
            render.SetPosition(1, ray.direction * 10f);
        }
        Rotate();
    }

    void Rotate()
    {
        transform.Rotate(0, 0, Time.deltaTime * turnSpeed);
    }

    void DamageTarget(RaycastHit2D hit, LineRenderer render)
    {
        var damage = hit.collider.GetComponent<IHittablle>();
        if (damage != null)
        {
            damage.ProcessDamage(1);
        }
        render.SetPosition(1, hit.point);
    }
}
