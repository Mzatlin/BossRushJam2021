using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDialogueBasedOnDay : SetNewDialogueBase
{
    public CurrentDaySO day;
    IDialogueSet setDialogue => GetComponent<IDialogueSet>();
    [TextArea(2, 3)]
    public List<string> firstDayDialogue;
    [TextArea(2, 3)]
    public List<string> secondDayDialogue;
    [TextArea(2, 3)]
    public List<string> thirdDayDialogue;
    [TextArea(2, 3)]
    public List<string> finalDayDialogue;
    // Start is called before the first frame update
    protected override void Awake()
    {
        index = day.currentDay;
        base.Awake();
    }
        
    protected override void InitializeListOfDialogue()
    {
        phaseDialogue.Add(firstDayDialogue);
        phaseDialogue.Add(secondDayDialogue);
        phaseDialogue.Add(thirdDayDialogue);
        phaseDialogue.Add(finalDayDialogue);
    }

    public void SetNextDialogue()
    {
        if (setDialogue != null)
        {
            setDialogue.SetContentText(phaseDialogue[index]);
        }
    }

}
