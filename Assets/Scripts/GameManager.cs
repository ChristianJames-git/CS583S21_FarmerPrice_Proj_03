using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject buildAreaModel, openAreaModel, WallNS, WallWE, Ground;
    public Transform BuildPlatforms, OpenPaths, Boundaries, End;
    public int mapX = 8, mapZ = 15;
    private GameObject[,] areaBlocks;

    public GameObject[] enemies;
    private string enemyTag = "Enemy";
    public Transform Bullets;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }


    // Start is called before the first frame update
    void Start()
    {
        areaBlocks = new GameObject[mapX, mapZ];
        //Generate build/move areas
        InstantiateBuildAreas();
        //Can replace this later with adding enemies to List directly anytime you add one
        InvokeRepeating("FindEnemies", 0f, 0.5f);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //Create Map
    private void InstantiateBuildAreas()
    {
        if (mapX < 2 || mapZ < 2)
            return; //TBI Throw error of map size not applicable here
        //Handle odd values
        float shiftX, shiftZ, endX;
        if (mapX % 2 == 0)
            shiftX = mapX / 2 - 0.5f;
        else
            shiftX = mapX / 2;
        if (mapZ % 2 == 0) {
            shiftZ = mapZ / 2 - 0.5f;
            endX = 0;
        } else {
            shiftZ = mapZ / 2;
            endX = mapX - 1;
        }
        //Center Ground Position
        Ground.transform.localPosition = new Vector3(shiftX, -0.5f, shiftZ);
        //Set WallNS
        WallNS.transform.localScale = new Vector3(mapX, 2, 0.2f);
        WallNS.transform.localPosition = new Vector3(shiftX, 0.3f, -0.6f);
        Instantiate(WallNS, Boundaries).transform.localPosition = new Vector3(shiftX, 0.3f, -0.4f + mapZ);
        //Set WallWE
        WallWE.transform.localScale = new Vector3(0.2f, 2, mapZ + 0.4f);
        WallWE.transform.localPosition = new Vector3(-0.6f, 0.3f, shiftZ);
        Instantiate(WallWE, Boundaries).transform.localPosition = new Vector3(-0.4f + mapX, 0.3f, shiftZ);
        //Set even rows starting on 0
        for (int i = 0; i < mapZ; i+=2)
        {
            if (i % 4 == 0)
            {
                OpenAreaBlock(0, i);
                BuildAreaBlock(mapX - 1, i);
            } else {
                BuildAreaBlock(0, i);
                OpenAreaBlock(mapX - 1, i);
            }
            for (int j = 1; j < mapX-1; j++)
                BuildAreaBlock(j, i);
        }
        buildAreaModel.SetActive(false);
        //Set odd rows starting on 1
        for (int i = 1; i < mapZ; i+=2)
            for (int j = 0; j < mapX; j++)
                OpenAreaBlock(j, i);
        openAreaModel.SetActive(false);
        //Move end
        End.position = new Vector3(endX, 0, mapZ - 1);
    }

    private void BuildAreaBlock(int x, int z)
    {
        GameObject buildArea = Instantiate(buildAreaModel, BuildPlatforms);
        buildArea.SetActive(true);
        buildArea.transform.position = new Vector3(x, -0.4f, z);
        //buildArea.transform.scale = new Vector3(1,1,1);
        areaBlocks[x, z] = buildArea;
    }

    private void OpenAreaBlock(int x, int z)
    {
        GameObject openArea = Instantiate(openAreaModel, OpenPaths);
        openArea.SetActive(true);
        openArea.transform.localPosition = new Vector3(x, -0.49f, z);
        areaBlocks[x, z] = openArea;
    }

    private void FindEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag(enemyTag);
    }

    public Transform FindNearestEnemy(float range, Transform turret)
    {
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                float distanceToEnemy = Vector3.Distance(turret.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
            return nearestEnemy.transform;
        else
            return null;
    }

    public Transform FindOldestEnemy(float range, Transform turret)
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                float distanceToEnemy = Vector3.Distance(turret.position, enemy.transform.position);
                if (distanceToEnemy <= range)
                    return enemy.transform;
            }
        }
        return null;
    }

    public Transform FindStrongestEnemy(float range, Transform turret)
    {
        float highestHealth = -1;
        GameObject strongestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                float enemyHealth = enemy.GetComponent<EnemyBase>().health;
                if (Vector3.Distance(turret.position, enemy.transform.position) <= range && enemyHealth > highestHealth)
                {
                    highestHealth = enemyHealth;
                    strongestEnemy = enemy;
                }
            }
        }
        if (strongestEnemy != null)
            return strongestEnemy.transform;
        else
            return null;
    }
}
