using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteOnDay : MonoBehaviour
{
    SpriteRenderer render;
    [SerializeField] CurrentDaySO day;
    [SerializeField] Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        if(day != null && render != null)
        {
            render.sprite = sprites[day.currentDay];
        }
    }

}
