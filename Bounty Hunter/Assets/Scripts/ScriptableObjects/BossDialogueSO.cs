using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "BossDialogueCheck")]
public class BossDialogueSO : ScriptableObject
{
    public bool isOpeningSet;

    public void ResetOpening()
    {
        isOpeningSet = false;
    }
}
