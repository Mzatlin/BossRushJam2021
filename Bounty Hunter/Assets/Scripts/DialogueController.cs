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
    IPortrait portrait;
    IDialogueActivate activate; 
    [TextArea(2, 3)]
    public List<string> content;
    [SerializeField]
    float typingSpeed = 0.3f;
    bool isActive = false;
    Coroutine dialogueCoroutine = null;
    int index = 0;

    public event Action OnDialogueEnd = delegate { };



    // Start is called before the first frame update
    void Awake()
    {
        activate = GetComponent<IDialogueActivate>();
        portrait = GetComponent<IPortrait>();
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
        content[index] = _content;
    }

    void HandleActivateDialogue()
    {
        if (dialogueCanvas != null && !dialoguewriter.IsWriting)
        {
            dialogueCanvas.enabled = true;
            isActive = true;
            StartWriting();
        }
    }

    void StartWriting()
    {
        dialoguewriter.RequestToWrite();
        if(portrait != null)
        {
            portrait.SetPortrait(index);
        }
        dialogueCoroutine = StartCoroutine(TypeDelay(content[index]));
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
                textDialogue.text = "";
                if (index >= content.Count - 1)
                {
                    OnDialogueEnd();
                    dialogueCanvas.enabled = false;
                    isActive = false;
                    dialoguewriter.ResetWriter();
                }
                else
                {
                    index++;
                    StartWriting();
                }
            }
            else
            {
                StopCoroutine(dialogueCoroutine);
                textDialogue.text = content[index];

            }

        }

    }

    bool IsTextFinished()
    {
        if (textDialogue.text == content[index])
        {
            return true;
        }
        return false;
    }

}
