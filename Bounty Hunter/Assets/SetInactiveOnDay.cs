using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInactiveOnDay : MonoBehaviour
{
    [SerializeField] CurrentDaySO day;

    private void Start()
    {
        if(day != null && day.currentDay >= 3)
        {
            gameObject.SetActive(false);
        }
    }
}
