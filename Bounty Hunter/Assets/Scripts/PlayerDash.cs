using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    Camera cam;
    bool canDash = true;
    bool isdashing = false;
    Vector2 mousePos;
    Vector2 dashDirection;
    float dashSpeed;
    IMovePhysics physics;
    [SerializeField] float maxDashSpeed = 100f;
    float timeThreshold = 0.3f;
    float dashDelay;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        dashSpeed = maxDashSpeed;
        physics = GetComponent<IMovePhysics>();
    }

    // Update is called once per frame
    void Update()
    {
      
        if(Time.time > dashDelay)
        {
            canDash = true;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            AttemptDash();
        }
        if (isdashing)
        {
            HandleDash();
        }

    }

    void HandleDash()
    {
        if (Vector2.Distance(mousePos, transform.position) < 3f)
        {
            isdashing = false;
            canDash = false;
            dashSpeed = maxDashSpeed;
            dashDelay = Time.time + timeThreshold;
        }
        else
        {
            //physics.SetMoveVelocity(dashDirection * dashSpeed);
            transform.position += (Vector3)(dashDirection * dashSpeed * Time.deltaTime);
            dashSpeed -= dashSpeed * 10f * Time.deltaTime;
        }
    }

    void AttemptDash()
    {
        if (canDash && !isdashing)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            dashDirection = (mousePos - (Vector2)transform.position).normalized;
            isdashing = true;
            canDash = false;
        }
    }
}
