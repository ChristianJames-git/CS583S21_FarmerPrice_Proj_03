using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private Color baseColor;
    public Color hoverColor;
    public Renderer rend;

    private GameObject turret;
    private Material turretBarrelColor;
    private int turretType = -1;
    private int turretLevel = 0;

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
        BuildManager.instance.AreaSelected(transform, this, turretType, turretLevel);
        BuildManager.instance.BuyTurret(0);
        BuildManager.instance.UpgradeTurret();
    }

    public void TurretBuilt(int type, GameObject turret)
    {
        this.turret = turret;
        turretType = type;
        turretLevel = 1;
        turretBarrelColor = this.turret.GetComponentInChildren<Renderer>().material;
        TurretUpgraded(1);
    }
    public void TurretUpgraded(int level)
    {
        turretLevel = level;
        switch (level)
        {
            case 1:
                baseColor = Color.yellow;
                break;
            case 2:
                baseColor = BuildManager.instance.orange;
                break;
            case 3:
                baseColor = Color.red;
                break;
            default:
                baseColor = Color.white;
                break;
        }
        if (level > 0)
            turretBarrelColor.color = baseColor;
    }
}
