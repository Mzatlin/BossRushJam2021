using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCurrentDayOnClick : MonoBehaviour
{
    [SerializeField] CurrentDaySO day;
    
    public void ResetCurrentDay()
    {
        if(day != null)
        {
            day.ResetDayCount();
        }
    }
}
