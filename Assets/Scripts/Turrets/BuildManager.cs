using System.Collections;
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
    private float[,] turretDamages; //[turret type, turret level]
    private float[,] turretFireRates;
    private float[,] turretRanges;
    private float[,] bulletDamageRadius;
    private float[,] bulletSpeeds;
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
        InstantiateTurretValues();
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

    private void InstantiateTurretValues()
    {
        turretDamages = new float[3, 3] { { 5, 10, 18 }, { 10, 30, 32 }, { 8, 10, 15 } };
        turretFireRates = new float[3, 3] { { 1, 1, 1 }, { 0.25f, 0.4f, 0.75f }, { 1, 2, 3 } };
        turretRanges = new float[3, 3] { { 3, 4, 5 }, { 4, 6, 8 }, { 2, 2, 3 } };
        bulletDamageRadius = new float[3, 3] { { 0, 0, 0 }, { 1, 1.5f, 2 }, { 0, 0, 0 } };
        bulletSpeeds = new float[3, 3] { { 20, 20, 20 }, { 5, 8, 10 }, { 40, 40, 40 } };
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

    //Buttons
    public void BuildTurret (Node script)
    {
        //Add money checking and subtraction
        GameObject turret = (GameObject)Instantiate(turretPrefabs[currentTurretDisplayed], script.transform.position, script.transform.rotation, TurretsContainer);
        script.turret = turret;
        script.turretScript = turret.GetComponent<Turret>();
        script.turretColor = turret.GetComponentInChildren<Renderer>().material;
        script.turretType = currentTurretDisplayed;
        script.turretScript.damage = turretDamages[currentTurretDisplayed, 0];
        script.turretScript.fireRate = turretFireRates[currentTurretDisplayed, 0];
        script.turretScript.range = turretRanges[currentTurretDisplayed, 0];
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
        script.turretScript.damage = turretDamages[script.turretType, script.turretLevel];
        script.turretScript.fireRate = turretFireRates[script.turretType, script.turretLevel];
        script.turretScript.range = turretRanges[script.turretType, script.turretLevel];
        script.turretScript.bulletDamageRadius = bulletDamageRadius[script.turretType, script.turretLevel];
        script.turretScript.bulletSpeed = bulletSpeeds[script.turretType, script.turretLevel];
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