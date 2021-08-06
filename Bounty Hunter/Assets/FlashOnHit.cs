using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashOnHit : MonoBehaviour
{
    [SerializeField]
    Color flashColor;
    Color originalColor;
    SpriteRenderer sprite;
    IHittablle hit;

    // Start is called before the first frame update
    void Start()
    {
        hit = GetComponent<IHittablle>();
        hit.OnHit += HandleHit;
        sprite = GetComponentInChildren<SpriteRenderer>();
        originalColor = sprite.color;
    }

    private void OnEnable()
    {
        sprite.color = originalColor;
    }

    private void HandleHit()
    {
        StartCoroutine(FlashDelay());
    }

    IEnumerator FlashDelay()
    {
        sprite.color = flashColor;
        yield return new WaitForSeconds(.1f);
        sprite.color = originalColor;
    }
}
