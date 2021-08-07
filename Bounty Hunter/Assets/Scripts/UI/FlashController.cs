using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashController : MonoBehaviour, IFlash
{
    [SerializeField] GameObject FlashObj;
    Animator animate;

    public void ActivateFlash()
    {
       if(animate != null)
        {
            animate.SetTrigger("Activate");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(FlashObj != null)
        {
            animate = FlashObj.GetComponentInChildren<Animator>();
        }

    }
}
