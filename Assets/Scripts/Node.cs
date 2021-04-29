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
    private Turret turretScript;

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
        if (BuildManager.instance.inBuildMode)
            BuildTurret();
        else if (BuildManager.instance.inUpgradeMode)
            UpgradeTurret();
        BuildManager.instance.AreaSelected(transform, this, turretType, turretLevel);
    }

    private void BuildTurret()
    {

    }

    private void UpgradeTurret()
    {

    }
    public void TurretBuilt(int type, GameObject turret)
    {
        //this.turret = turret;
        turretType = type;
        turretLevel = 1;
        turretBarrelColor = turret.GetComponentInChildren<Renderer>().material;
        turretScript = turret.GetComponent<Turret>();
        TurretUpgraded(1);
    }
    public void TurretUpgraded(int level)
    {
        turretLevel = level;
        switch (level)
        {
            case 1:
                baseColor = Color.yellow;
                turretScript.damage = 5;
                break;
            case 2:
                baseColor = BuildManager.instance.orange;
                turretScript.damage = 10;
                break;
            case 3:
                baseColor = Color.red;
                turretScript.damage = 20;
                break;
            default:
                baseColor = Color.white;
                break;
        }
        if (level > 0)
            turretBarrelColor.color = baseColor;
    }
}
