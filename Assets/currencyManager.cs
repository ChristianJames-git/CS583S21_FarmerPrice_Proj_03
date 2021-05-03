using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class currencyManager : MonoBehaviour
{
    public int currentBal;
    public TextMeshProUGUI Balance;

    private void Awake()
    {
        currentBal = 700;
    }

    private void Update()
    {
        Balance.text = currentBal.ToString();
    }

    public bool balanceCheck(int turretPrice)
    {
        if (currentBal >= turretPrice)
            return true;
        else
            return false;
    }


}
