using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateTextFade : MonoBehaviour
{

    Animator animate;
    IDialogueEnd end => GetComponent<IDialogueEnd>();
    [SerializeField] GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        animate = text.GetComponent<Animator>();
        if (end != null)
        {
            end.OnDialogueEnd += HandleDialogueEnd;
        }
    }

    private void OnDestroy()
    {
        if (end != null)
        {
            end.OnDialogueEnd -= HandleDialogueEnd;
        }
    }
    private void HandleDialogueEnd()
    {
        if (animate != null)
        {
            animate.SetTrigger("Activate");
        }
    }

}
