using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSplashScreenOnDialogueEnd : MonoBehaviour
{
    Animator splashAnimator, fadeAnimator, flashAnimator;
    IDialogueEnd end => GetComponent<IDialogueEnd>();
    [SerializeField] GameObject splashScreen;
    [SerializeField] GameObject flashScreen;
    [SerializeField] BossDialogueSO bossDialogue;
    [SerializeField] PlayerStatsSO stats;

    // Start is called before the first frame update
    void Start()
    {
        if (splashScreen != null)
        {
            fadeAnimator = splashScreen.GetComponent<Animator>();
            splashAnimator = splashScreen.GetComponentInChildren<Animator>();
            splashScreen.SetActive(false);
        }
        if (flashScreen != null)
        {
            flashAnimator = flashScreen.GetComponent<Animator>();
        }
        if (end != null && bossDialogue != null && !bossDialogue.isOpeningSet)
        {
            end.OnDialogueEnd += HandleDialogueEnd;
        }
    }

    private void HandleDialogueEnd()
    {
        if (stats != null)
        {
            stats.isInDialogue = true;
        }
        if (flashAnimator != null)
        {
            flashAnimator.SetTrigger("Activate");
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.5f);
        splashScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        if (fadeAnimator != null)
        {
            fadeAnimator.SetTrigger("Activate");
        }
        yield return new WaitForSeconds(1f);
        if (stats != null)
        {
            stats.isInDialogue = false;
            stats.isReady = true;
        }
    }

    private void OnDestroy()
    {
        if (end != null)
        {
            end.OnDialogueEnd -= HandleDialogueEnd;
        }
    }
}
