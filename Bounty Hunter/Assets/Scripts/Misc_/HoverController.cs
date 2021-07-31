using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverController : MonoBehaviour
{
    Vector2 startPos;
    private void OnEnable()
    {
        startPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x, Mathf.Cos(Time.time)/4f);
    }

}
