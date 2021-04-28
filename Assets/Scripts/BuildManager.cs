using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public GameObject turret1;
    private GameObject areaSelected;
    private int turretType;
    private int turretLevel;

    public Color orange;

    [Header("UI")]
    public GameObject TurretUI;

    void Awake()
    {
        instance = this;
        orange = new Color(1.0f, 0.64f, 0.0f);
    }

    public void AreaSelected(GameObject areaClicked, int turretType, int turretLevel)
    {
        areaSelected = areaClicked;
        this.turretLevel = turretLevel;
        this.turretType = turretType;
    }

    public void BuyTurret (int turretType)
    {

    }

    public void UpgradeTurret()
    {

    }

    public void SellTurret()
    {

    }
}
