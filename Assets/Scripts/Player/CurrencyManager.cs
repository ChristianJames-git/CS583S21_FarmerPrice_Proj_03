using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    private int currentBal;
    public TextMeshProUGUI Balance;

    private void Start()
    {
        instance = this;
        currentBal = 700;
        UpdateHUD();
    }

    public bool balanceCheck(int turretPrice)
    {
        if (currentBal >= turretPrice)
            return true;
        else
            return false;
    }

    public void inputMoney(int money)
    {
        if (money >= 0)
            currentBal += money;
        UpdateHUD();
    }

    public void outputMoney(int money)
    {
        if (money >= 0)
            currentBal -= money;
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        Balance.text = currentBal.ToString();
    }
}
