using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MoneyController")]
public class MoneySO : ScriptableObject
{
    public int MoneyTotal;

    public void UpdateMoney(int amount)
    {
        MoneyTotal += amount;
    }

}
