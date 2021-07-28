using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    public float leftBorder;
    public float rightBorder;
    public float topBorder;
    public float bottomBorder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= leftBorder)
        {
          transform.position = new Vector2(leftBorder, transform.position.y);
        }
        if(transform.position.x >= rightBorder)
        {
           transform.position = new Vector2(rightBorder, transform.position.y);
        }
        if (transform.position.y <= bottomBorder)
        {
            transform.position = new Vector2(bottomBorder, transform.position.y);
        }
        if (transform.position.y >= topBorder)
        {
            transform.position = new Vector2(topBorder, transform.position.y);
        }
    }
}
