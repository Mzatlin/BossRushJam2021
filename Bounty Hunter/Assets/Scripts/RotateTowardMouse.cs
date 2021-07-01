using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardMouse : MonoBehaviour
{
    Quaternion gunRotation;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RotateGun();
    }

    void RotateGun()
    {
        //Find the displacement vector from the Object to where the mouse is in WorldSpace
        Vector2 direction = (cam.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        //Using the magic of polar coordinates, we take the previous vector and convert it to an angle of rotation in Euler Angles
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Set the rotation of the object to this quaterntion rotation along the Z axis (any other rotation will look weird in 2D)
        gunRotation.eulerAngles = new Vector3(0, 0, angle);
        transform.rotation = gunRotation;

        AdjustLocalScale(angle);
    }

    void AdjustLocalScale(float angle)
    {
        //To ensure the rotating object never appears upside-down, the y scale is inverted based on its current rotation
        Vector3 aimLocalScale = transform.localScale;
        if (angle > 90 || angle < -90)
        {
            aimLocalScale.y = -1f;
        }
        else
        {
            aimLocalScale.y = +1f;
        }
        transform.localScale = aimLocalScale;
    }
}
