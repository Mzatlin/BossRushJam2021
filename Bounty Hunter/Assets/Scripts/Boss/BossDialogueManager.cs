using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDialogueManager : SetNewDialogueBase
{
    IDialogueSet setDialogue => GetComponent<IDialogueSet>();
    public BossDialogueSO bossDialogue;
    [TextArea(2, 3)]
    public List<string> firstPhaseDialogue;
    [TextArea(2, 3)]
    public List<string> secondPhaseDialogue;
    [TextArea(2, 3)]
    public List<string> thirdphaseDialogue;
  

    // Start is called before the first frame update
    protected override void Awake()
    {
        if (bossDialogue != null && bossDialogue.isOpeningSet)
        {
            index = 1;
        }
        else
        {
            index = 0;
        }
        base.Awake();
    }

    protected override void InitializeListOfDialogue()
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
