using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AfterImageBase : MonoBehaviour
{
    [SerializeField] protected float lifeTime = 0.1f;
    [SerializeField] protected float baseAlpha = 0.8f;
    protected float spriteAlpha;
    protected float alphaMultiplier = 0.85f;
    protected float timeActive;

    protected Transform sourceTarget;
    protected SpriteRenderer render;
    protected SpriteRenderer sourceTargetRender;

    protected Color spriteColor;

    protected abstract void FindTargetObject();

    protected void SetupAfterImage()
    {
        render = GetComponent<SpriteRenderer>();
        spriteAlpha = baseAlpha;
        render.sprite = sourceTargetRender.sprite;
        transform.position = sourceTarget.position;
        transform.rotation = sourceTarget.rotation;
        timeActive = Time.time;
    }

    protected void DegradeAfterImage()
    {
        spriteAlpha *= alphaMultiplier;
        spriteColor = new Color(1f, 1f, 1f, spriteAlpha);
        render.color = spriteColor;
        if (Time.time >= (timeActive + lifeTime))
        {
            gameObject.SetActive(false);
        }
    }
}
