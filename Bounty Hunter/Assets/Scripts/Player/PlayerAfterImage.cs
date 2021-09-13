using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImage : AfterImageBase
{
    protected override void FindTargetObject()
    {
        sourceTarget = FindObjectOfType<PlayerMovementController>().transform;
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
