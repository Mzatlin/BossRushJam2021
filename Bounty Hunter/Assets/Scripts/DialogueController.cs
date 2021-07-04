using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour, IDialogueEnd
{
    public Canvas dialogueCanvas;
    public PlayerStatsSO playerStats;
    public TextMeshProUGUI textDialogue;
    public DialogueHandlerSO dialoguewriter;
    [TextArea(2, 3)]
    public string content;
    [SerializeField]
    float typingSpeed = 0.3f;
    bool isActive = false;
    Coroutine dialogueCoroutine = null;

    public event Action OnDialogueEnd = delegate { };


    IDialogueActivate activate; 

    // Start is called before the first frame update
    void Awake()
    {
        activate = GetComponent<IDialogueActivate>();

        if (dialoguewriter == null)
        {
            Debug.Log(gameObject.name + " has a dialoguecontroller without the dialogueSO!");
        }

        if (dialogueCanvas != null && textDialogue != null)
        {
            dialogueCanvas.enabled = false;
            textDialogue.text = "";
        }
        else
        {
            Debug.Log("Dialogue Canvas/Text Dialogue not attached to: " + gameObject);
        }

        if (activate != null)
        {
            activate.OnDialogueStart += HandleActivateDialogue;
        }
    }

    void OnDestroy()
    {
        if (activate != null)
        {
            activate.OnDialogueStart -= HandleActivateDialogue;
        }
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
        }
    }


    public void SetContentText(string _content)
    {
        content = _content;
    }


    void HandleActivateDialogue()
    {
        if (dialogueCanvas != null && !dialoguewriter.IsWriting)
        {
            dialogueCanvas.enabled = true;
            isActive = true;
            dialoguewriter.RequestToWrite();
            dialogueCoroutine = StartCoroutine(TypeDelay(content));
        }
    }

    IEnumerator TypeDelay(string sentence)
    {
        var newtypingSpeed = typingSpeed / 10;
        foreach (char letter in sentence)
        {
            textDialogue.text += letter;
            yield return new WaitForSeconds(newtypingSpeed);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            EndDialogueOnInput();
        }
    }

    void EndDialogueOnInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerStats != null && !playerStats.isReady)
        {
            if (IsTextFinished())
            {
                OnDialogueEnd();
                dialogueCanvas.enabled = false;
                textDialogue.text = "";
                isActive = false;
                dialoguewriter.ResetWriter();
            }
            else
            {
                StopCoroutine(dialogueCoroutine);
                textDialogue.text = content;
            }

        }

    }

    bool IsTextFinished()
    {
        if (textDialogue.text == content)
        {
            return true;
        }
        return false;
    }

}
