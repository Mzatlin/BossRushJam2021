using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossAfterImage : AfterImageBase
{
    protected override void FindTargetObject()
    {
        sourceTarget = FindObjectOfType<FirstBossAI>().transform;
        sourceTargetRender = sourceTarget.GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        FindTargetObject();
        SetupAfterImage();
    }

    private void Update()
    {
        DegradeAfterImage();
    }

}
