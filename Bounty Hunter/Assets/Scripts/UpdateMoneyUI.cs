using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateMoneyUI : MonoBehaviour
{
    [SerializeField] MoneySO money;
    [SerializeField] TextMeshProUGUI moneyUI;
    // Start is called before the first frame update
    void Start()
    {
        if (money != null && moneyUI != null)
        {
            SetMoney();
        }
    }

    void SetMoney()
    {
        moneyUI.SetText("$" + money.MoneyTotal.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (money != null && moneyUI != null)
        {
            SetMoney();
        }

    }
}
