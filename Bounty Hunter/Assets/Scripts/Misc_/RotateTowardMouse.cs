using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardMouse : GunRotationBase
{
    Camera cam;
    [SerializeField] PlayerStatsSO playerStats;
    // Start is called before the first frame update
    protected override void Start()
    {
        cam = Camera.main;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStats != null && playerStats.GetPlayerReadiness())
        {
            RotateGun();
        }
    }

    protected override void RotateGun()
    {
        //Find the displacement vector from the Object to where the mouse is in WorldSpace
        Vector2 direction = (cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.z)) - transform.position).normalized;
        //Using the magic of polar coordinates, we take the previous vector and convert it to an angle of rotation in Euler Angles
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Set the rotation of the object to this quaterntion rotation along the Z axis (any other rotation will look weird in 2D)
        gunRotation.eulerAngles = new Vector3(0, 0, angle);
        transform.rotation = gunRotation;

        AdjustLocalScale(angle);
    }
}
