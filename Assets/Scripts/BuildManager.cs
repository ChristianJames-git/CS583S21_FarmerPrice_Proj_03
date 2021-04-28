using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public Transform TurretsContainer;
    public GameObject turret1;
    private Transform areaSelected;
    private Node areaScript;
    private int turretType;
    private int turretLevel;
    private List<GameObject> turretModels;

    public Color orange;

    [Header("UI")]
    public GameObject TurretUI;

    void Awake()
    {
        instance = this;
        turretModels = new List<GameObject>() { turret1 };
        TurretUI.SetActive(false);
    }

    public void AreaSelected(Transform areaClicked, Node areaScript, int turretType, int turretLevel)
    {
        TurretUI.SetActive(true);
        areaSelected = areaClicked;
        this.turretLevel = turretLevel;
        this.turretType = turretType;
        this.areaScript = areaScript;
    }

    //Buttons
    public void BuyTurret (int turretType)
    {
        if (turretLevel != 0)
            return;
        //Add money checking and subtraction
        this.turretType = turretType;
        turretLevel = 1;
        GameObject turret = (GameObject)Instantiate(turretModels[turretType], areaSelected.position, areaSelected.rotation, TurretsContainer);
        areaScript.TurretBuilt(turretType, turret);
    }

    public void UpgradeTurret()
    {
        if (turretLevel == 1 || turretLevel == 2)
            areaScript.TurretUpgraded(++turretLevel);
        //Add money checking and subtraction and damage upgrade
    }

    public void SellTurret()
    {

    }
}
