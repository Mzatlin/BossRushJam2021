using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambleOnClick : MonoBehaviour
{
    [SerializeField] MoneySO money;
    [SerializeField] Canvas board;

    public void Gamble()
    {
        float attempt = UnityEngine.Random.Range(0, 101);
        if(attempt < 2)
        {
            money.UpdateMoney(1000);
        }
        else
        {
            money.UpdateMoney(-100);
        }

        if(money.MoneyTotal < 1)
        {
            board.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
