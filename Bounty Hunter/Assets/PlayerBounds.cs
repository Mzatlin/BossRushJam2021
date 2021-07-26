using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= -10f)
        {
          //  transform.position = new Vector2(-10f, transform.position.y);
        }
        if(transform.position.x >= 9f)
        {
           // transform.position = new Vector2(9f, transform.position.y);
        }
    }
}
