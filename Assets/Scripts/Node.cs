using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color baseColor;
    public Color hoverColor;
    public Renderer rend;

    public GameObject turret = null;
    public Material turretBarrelColor;
    public int turretType = -1;
    public int turretLevel = 0;
    public Turret turretScript;

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
        if (BuildManager.instance.inBuildMode && turret == null)
            BuildManager.instance.BuildTurret(this);
        if (BuildManager.instance.inUpgradeMode && turret != null && turretLevel < 3)
            BuildManager.instance.UpgradeTurret(this);
        if (BuildManager.instance.inSellMode && turret != null)
            BuildManager.instance.SellTurret(this);
    }
}
