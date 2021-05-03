using UnityEngine;

public class Node : MonoBehaviour
{
    public Renderer rend;

    [Header("Colors")]
    public Color baseColor;
    public Color hoverColor;

    [Header("Turret References")]
    public GameObject turret = null;
    public Material turretColor;
    public int turretType = -1;
    public int turretLevel = 0;
    public TurretBase turretScript;
    public int turretPrice;
    public int upgradePrice;

    private GameObject currencyManager;

    private void Awake()
    {
        currencyManager = GameObject.Find("CurrencyManager");
        turretPrice = 100;
        upgradePrice = 100;
    }

    private void Start()
    {
        baseColor = rend.material.color;
    }

    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = baseColor;
    }

    private void OnMouseDown()
    {
        //build
        if (currencyManager.GetComponent<currencyManager>().balanceCheck(turretPrice))
        {
            if (BuildManager.instance.inBuildMode && turret == null)
            {
                currencyManager.GetComponent<currencyManager>().currentBal -= turretPrice;
                BuildManager.instance.BuildTurret(this);
            }
                
        }

        //upgrade
        if (currencyManager.GetComponent<currencyManager>().balanceCheck(upgradePrice))
        {
            if (BuildManager.instance.inUpgradeMode && turret != null && turretLevel < 3)
            {
                currencyManager.GetComponent<currencyManager>().currentBal -= upgradePrice;
                BuildManager.instance.UpgradeTurret(this);
            }
        }

        //sell
        if (BuildManager.instance.inSellMode && turret != null)
        {
            currencyManager.GetComponent<currencyManager>().currentBal += turretPrice;
            BuildManager.instance.SellTurret(this);
        }
            
    }
}
