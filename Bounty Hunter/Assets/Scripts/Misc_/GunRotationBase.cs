using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunRotationBase : MonoBehaviour, IGunRotate
{
    protected Quaternion gunRotation;
    protected SpriteRenderer render;
    protected int sortOrder;
    int underOrder;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        render = GetComponentInChildren<SpriteRenderer>();
        sortOrder = render.sortingOrder;
        underOrder = sortOrder - sortOrder;
    }

    protected abstract void RotateGun();

    public void AdjustLocalScale(float angle)
    {
        if(render == null)
        {
            return;
        }
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
        if (angle < 0)
        {
            render.sortingOrder = sortOrder;
        }
        else
        {
            render.sortingOrder = underOrder;
        }


        transform.localScale = aimLocalScale;
    }
}
