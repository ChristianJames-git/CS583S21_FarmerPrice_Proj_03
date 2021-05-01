using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    [Header("Turrets")]
    public Transform TurretsContainer;
    public GameObject turret1, turret2, turret3;
    private List<GameObject> turretPrefabs;
    public Transform Bullets;

    [Header("Modes")]
    public bool inBuildMode;
    public bool inUpgradeMode;
    public bool inSellMode;
    private Color orange;
 
    [Header("UI")]
    public GameObject ModeToggleUI;
    public Image B, U, Q, E, R;
    public Sprite B_Up, B_Down, U_Up, U_Down, Q_Up, Q_Down, E_Up, E_Down, R_Up, R_Down;

    [Header("Turret Display")]
    public Image turretToBuild;
    public Sprite turret1Sprite, turret2Sprite, turret3Sprite;
    private List<Sprite> turretSprites;
    private int currentTurretDisplayed = 0;

    void Awake()
    {
        instance = this;
        turretPrefabs = new List<GameObject>() { turret1, turret2, turret3 };
        turretSprites = new List<Sprite>() { turret1Sprite, turret2Sprite, turret3Sprite };
        orange = new Color(1, 0.45f, 0, 1);
    }

    private void Update()
    {
        //Toggle Build Mode
        if (Input.GetKeyDown(KeyCode.B))
            BPress(inBuildMode);
        //Toggle Upgrade Mode
        if (Input.GetKeyDown(KeyCode.U))
            UPress(inUpgradeMode);
        //Toggle Sell Mode
        if (Input.GetKeyDown(KeyCode.R))
            RPress(inSellMode);
        //Animate UI
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Q.color = Color.cyan;
            Q.sprite = Q_Down;
            if (currentTurretDisplayed > 0)
                turretToBuild.sprite = turretSprites[--currentTurretDisplayed];
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
            if (currentTurretDisplayed < turretSprites.Count-1)
                turretToBuild.sprite = turretSprites[++currentTurretDisplayed];
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            E.color = Color.white;
            E.sprite = E_Up;
        }
    }

    //Mode Toggle Buttons
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
            RPress(true);
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
            RPress(true);
            U.sprite = U_Down;
        }
    }
    private void RPress(bool inMode)
    {
        if (inMode)
        {
            R.color = Color.white;
            inSellMode = false;
            R.sprite = R_Up;
        }
        else
        {
            R.color = Color.cyan;
            inSellMode = true;
            BPress(true);
            UPress(true);
            R.sprite = R_Down;
        }
    }

    //Mode Effects
    public void BuildTurret (Node script)
    {
        //Add money checking and subtraction
        GameObject turret = (GameObject)Instantiate(turretPrefabs[currentTurretDisplayed], script.transform.position, script.transform.rotation, TurretsContainer);
        script.turret = turret;
        script.turretScript = turret.GetComponent<TurretBase>();
        script.turretScript.UpdateStats(0);
        script.turretColor = turret.GetComponentInChildren<Renderer>().material;
        script.turretType = currentTurretDisplayed;
        UpgradeTurret(script);
    }
    public void UpgradeTurret(Node script)
    {
        Color newColor;
        //Add money checking and subtraction and damage upgrade
        switch (script.turretLevel)
        {
            case 0:
                newColor = Color.yellow;
                break;
            case 1:
                newColor = orange;
                break;
            case 2:
                newColor = Color.red;
                break;
            default:
                newColor = Color.white;
                break;
        }
        script.turretScript.UpdateStats(script.turretLevel);
        script.turretColor.color = newColor;
        script.baseColor = newColor;
        script.turretLevel++;
    }
    public void SellTurret(Node script)
    {
        Destroy(script.turret);
        script.turret = null;
        script.baseColor = Color.white;
        script.turretLevel = 0;
    }
}