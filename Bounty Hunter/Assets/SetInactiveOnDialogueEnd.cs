using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInactiveOnDialogueEnd : MonoBehaviour
{
    IDialogueEnd end;
    // Start is called before the first frame update
    void Start()
    {
        end = GetComponent<IDialogueEnd>();
        end.OnDialogueEnd += HandleEnd;
    }

    private void HandleEnd()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (end != null)
        {
            end.OnDialogueEnd -= HandleEnd;
        }
    }

}
