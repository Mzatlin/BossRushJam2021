using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutOnDialogueEnd : MonoBehaviour
{
    [SerializeField] GameObject fadeObject;
    Animator fadeAnimator;
    IDialogueEnd end => GetComponent<IDialogueEnd>();
    // Start is called before the first frame update
    void Start()
    {
        if (fadeObject != null)
        {
            fadeAnimator = fadeObject.GetComponentInChildren<Animator>();
        }
        if (end != null)
        {
            end.OnDialogueEnd += HandleDialogueEnd;
        }
    }

    private void OnDestroy()
    {
        if(end != null)
        {
            end.OnDialogueEnd -= HandleDialogueEnd;
        }
    }

    void HandleDialogueEnd()
    {
        if (fadeAnimator != null)
        {
            fadeAnimator.SetTrigger("FadeOut");
        }
    }

}
