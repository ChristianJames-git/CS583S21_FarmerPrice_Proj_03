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
        
        if (BuildManager.instance.inBuildMode && turret == null)
            BuildManager.instance.BuildTurret(this);

        //upgrade
        if (BuildManager.instance.inUpgradeMode && turret != null && turretLevel < 3)
            BuildManager.instance.UpgradeTurret(this);

        //sell
        if (BuildManager.instance.inSellMode && turret != null)
            BuildManager.instance.SellTurret(this);
    }
}
