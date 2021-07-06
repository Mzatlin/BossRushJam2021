using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmineFlickerAnimation : MonoBehaviour
{
    Animator anim;
    IDetonate detonate;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        detonate = GetComponent<IDetonate>();
        if(detonate != null)
        {
            detonate.OnDetonate += HandleDetonation;
        }
    }

    private void OnDestroy()
    {
        if (detonate != null)
        {
            detonate.OnDetonate -= HandleDetonation;
        }
    }

    private void HandleDetonation()
    {
        anim.SetTrigger("Detonate");
    }
}
