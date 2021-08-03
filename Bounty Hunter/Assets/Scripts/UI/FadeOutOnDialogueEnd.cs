using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutOnDialogueEnd : MonoBehaviour
{
    [SerializeField] GameObject fadeObject;
    [SerializeField] float fadeDelay = 2f;
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
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(fadeDelay);
        if (fadeAnimator != null)
        {
            fadeAnimator.SetTrigger("FadeOut");
        }
    }

}
