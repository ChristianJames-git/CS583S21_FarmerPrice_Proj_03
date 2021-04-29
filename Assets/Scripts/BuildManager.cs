using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public Transform TurretsContainer;
    public GameObject turret1; //turret2, turret3
    private List<GameObject> turretPrefabs;
    private float[,] turretDamages; //[turret type, turret level]

    public bool inBuildMode;
    public bool inUpgradeMode;
    private Color orange;

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
    private int currentTurretDisplayed = 0;
    public Image turretToBuild;

    void Awake()
    {
        instance = this;
        turretPrefabs = new List<GameObject>() { turret1 };
        turretSprites = new List<Sprite>() { turret1Sprite, turret2Sprite, turret3Sprite };
        turretDamages = new float[1, 3] { { 5, 10, 20 } };
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

    //Buttons
    public void BuildTurret (Node script)
    {
        //Add money checking and subtraction
        GameObject turret = (GameObject)Instantiate(turretPrefabs[currentTurretDisplayed], script.transform.position, script.transform.rotation, TurretsContainer);
        script.turret = turret;
        script.turretScript = turret.GetComponent<Turret>();
        script.turretBarrelColor = turret.GetComponentInChildren<Renderer>().material;
        script.turretType = currentTurretDisplayed;
        script.turretLevel = 1;
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
        script.turretScript.damage = turretDamages[script.turretType, script.turretLevel++];
        script.turretBarrelColor.color = newColor;
        script.baseColor = newColor;
    }

    public GameObject SellTurret(GameObject turret)
    {
        Destroy(turret);
        return null;
    }
}
