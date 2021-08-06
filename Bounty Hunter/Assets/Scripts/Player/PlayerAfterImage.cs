using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    [SerializeField] float lifeTime = 0.1f;
    [SerializeField] float baseAlpha = 0.8f;
    float spriteAlpha;
    float alphaMultiplier = 0.85f;
    float timeActive;

    Transform player;
    SpriteRenderer render;
    SpriteRenderer playerRender;

    Color spriteColor;

    private void OnEnable()
    {
        render = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerMovementController>().transform;
        playerRender = player.GetComponentInChildren<SpriteRenderer>();

        spriteAlpha = baseAlpha;
        render.sprite = playerRender.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        timeActive = Time.time;
    }

    private void Update()
    {
        spriteAlpha *= alphaMultiplier;
        spriteColor = new Color(1f, 1f, 1f, spriteAlpha);
        render.color = spriteColor;
        if(Time.time >= (timeActive + lifeTime))
        {
            gameObject.SetActive(false);
        }
    }
}
