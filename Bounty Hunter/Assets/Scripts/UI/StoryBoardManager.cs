using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryBoardManager : MonoBehaviour
{
    [SerializeField] CurrentDaySO day;
    [SerializeField] List<int> startingPortraits = new List<int>();
    SetDialoguePortrait portrait;
    // Start is called before the first frame update
    void Start()
    {
        portrait = GetComponent<SetDialoguePortrait>();
        if(portrait != null)
        {
            portrait.SetPortrait(startingPortraits[day.currentDay]);
        }
    }

}
