using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDialogueWriterOnAwake : MonoBehaviour
{

    [SerializeField] DialogueHandlerSO dialogue;
    // Start is called before the first frame update
    void Awake()
    {
        dialogue.ResetWriter();
    }
}
