using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnDialogueEnd : MonoBehaviour
{
    IDialogueEnd dialogueEnd;
    public float loadDelay = 6f;
    [SerializeField] int levelIndex = 3;
    [SerializeField] CurrentDaySO day;
    // Start is called before the first frame update
    void Start()
    {
        dialogueEnd = GetComponent<IDialogueEnd>();
        if(dialogueEnd != null)
        {
            dialogueEnd.OnDialogueEnd += HandleDialogueEnd;
        }
    }

    private void OnDestroy()
    {
        if(dialogueEnd != null)
        {
            dialogueEnd.OnDialogueEnd -= HandleDialogueEnd;
        }
    }

    private void HandleDialogueEnd()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(loadDelay);
        if (day != null && day.currentDay >= 3)
        {
            SceneManager.LoadScene(8);
        }
        else
        {
            SceneManager.LoadScene(levelIndex);
        }
    }
}
