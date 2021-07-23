using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDialogueManager : MonoBehaviour
{
    IDialogueSet setDialogue => GetComponent<IDialogueSet>();
    [TextArea(2, 3)]
    public List<string> firstPhaseDialogue;
    [TextArea(2, 3)]
    public List<string> secondPhaseDialogue;
    [TextArea(2, 3)]
    public List<string> thirdphaseDialogue;
    List<List<string>> phaseDialogue = new List<List<string>>();

    public BossDialogueSO bossDialogue;

    int index = 0;


    // Start is called before the first frame update
    void Awake()
    {
        if(bossDialogue != null && bossDialogue.isOpeningSet)
        {
            index = 1;
        }
        InitializeListOfDialogue();
    }

    void InitializeListOfDialogue()
    {
        phaseDialogue.Add(firstPhaseDialogue);
        phaseDialogue.Add(secondPhaseDialogue);
        phaseDialogue.Add(thirdphaseDialogue);
    }

    public void SetNextDialogue()
    {
        if(setDialogue != null)
        {
            setDialogue.SetContentText(phaseDialogue[index]);
            index++;
        }
    }

}
