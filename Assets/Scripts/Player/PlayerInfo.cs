using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInfo : MonoBehaviour
{
    //Maybe a PlayerStats?

    //Keeps track of money and any person upgrades? 
    //We don't need this unless we make the character personalization more complex
    public static PlayerInfo instance;
    private int coreHealth;
    private int maxCoreHealth;
    public Image healthBar;

    private void Awake()
    {
        instance = this;
    }

    public void SetCoreHealth(int newCoreHealth)
    {
        if (newCoreHealth < 1)
            newCoreHealth = 1;
        maxCoreHealth = newCoreHealth;
        coreHealth = newCoreHealth;
        UpdateBar();
    }

    public int GetCoreHealth()
    {
        return coreHealth;
    }

    public void DamageCore(int damage)
    {
        coreHealth -= damage;
        UpdateBar();
    }

    public void UpdateBar()
    {
        float percent = (float)coreHealth / maxCoreHealth;
        healthBar.fillAmount = percent;
        if (percent > 0.5)
            healthBar.color = Color.green;
        else if (percent > 0.25 && percent <= 0.5)
            healthBar.color = Color.yellow;
        else if (percent > 0 && percent <= 0.25)
            healthBar.color = Color.red;
        else
            GameManager.Instance.ChangeScene("YouLose");
    }
}
