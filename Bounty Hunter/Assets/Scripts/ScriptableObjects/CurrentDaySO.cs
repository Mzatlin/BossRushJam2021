using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "DayChecker")]
public class CurrentDaySO : ScriptableObject
{
    public int currentDay;

    public void ResetDayCount()
    {
        currentDay = 0;
    }

    public void IncrementDayCount()
    {
        currentDay += 1;
    }
}
