using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public bool inBuildMode;
    public bool inUpgradeMode;

    public Color orange;

    [Header("UI")]
    public GameObject TurretUI;
    public Image B;
    public Sprite B_Up;
    public Sprite B_Down;
    public Image U;
    public Sprite U_Up;
    public Sprite U_Down;
    public Image Q;
    public Sprite Q_Up;
    public Sprite Q_Down;
    public Image E;
    public Sprite E_Up;
    public Sprite E_Down;
    public Sprite turret1Sprite, turret2Sprite, turret3Sprite;
    private List<Sprite> turretSprites;
    private int currentTurretDisplayed = 1;
    public Image turretToBuild;

    void Awake()
    {
        instance = this;
        turretModels = new List<GameObject>() { turret1 };
        turretSprites = new List<Sprite>() { turret1Sprite, turret2Sprite, turret3Sprite };
        TurretUI.SetActive(false);
    }

    private void Update()
    {
        //Toggle Build Mode
        if (Input.GetKeyDown(KeyCode.B))
            BPress(inBuildMode);
        //Toggle Upgrade Mode
        if (Input.GetKeyDown(KeyCode.U))
            UPress(inUpgradeMode);
        //Animate UI
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Q.color = Color.cyan;
            Q.sprite = Q_Down;
            if (currentTurretDisplayed > 1)
                turretToBuild.sprite = turretSprites[--currentTurretDisplayed-1];
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Q.color = Color.white;
            Q.sprite = Q_Up;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            E.color = Color.cyan;
            E.sprite = E_Down;
            if (currentTurretDisplayed < turretSprites.Count)
                turretToBuild.sprite = turretSprites[currentTurretDisplayed++];
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            E.color = Color.white;
            E.sprite = E_Up;
        }
    }

    private void BPress(bool inMode)
    {
        if (inMode)
        {
            B.color = Color.white;
            inBuildMode = false;
            B.sprite = B_Up;
        }
        else
        {
            B.color = Color.cyan;
            inBuildMode = true;
            UPress(true);
            B.sprite = B_Down;
        }
    }

    private void UPress(bool inMode)
    {
        if (inMode)
        {
            U.color = Color.white;
            inUpgradeMode = false;
            U.sprite = U_Up;
        }
        else
        {
            U.color = Color.cyan;
            inUpgradeMode = true;
            BPress(true);
            U.sprite = U_Down;
        }
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
