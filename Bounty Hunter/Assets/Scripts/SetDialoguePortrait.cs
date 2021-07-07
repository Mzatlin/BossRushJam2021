using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDialoguePortrait : MonoBehaviour, IPortrait
{
    [SerializeField] Image portrait;
    [SerializeField] Sprite[] sprites;
    Sprite lastPortrait;

    public void SetPortrait(int index)
    {
        if(index < sprites.Length)
        {
            lastPortrait = sprites[index];
            portrait.sprite = sprites[index];
        }
        else
        {
            if(lastPortrait != null)
            {
                portrait.sprite = lastPortrait;
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        lastPortrait = sprites[0];
        if(portrait == null)
        {
            Debug.Log("No Image Assigned!");
        }
    }


}
