using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOnStart : MonoBehaviour
{
    [SerializeField] GameObject fadeObject;
    Animator fadeAnimator;
    // Start is called before the first frame update
    void Start()
    {
        if(fadeObject != null)
        {
            fadeAnimator = fadeObject.GetComponentInChildren<Animator>();
        }

        if(fadeAnimator != null)
        {
            fadeAnimator.SetTrigger("FadeIn");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
