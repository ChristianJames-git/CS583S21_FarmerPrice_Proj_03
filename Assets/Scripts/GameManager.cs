using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public GameObject[] enemies;
    public bool paused;
    private string enemyTag = "Enemy";
    //public float timer;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }

    public void ChangeScene(string sceneName)
    {
        if (sceneName == "GameScene")
            InvokeRepeating("FindEnemies", 0f, 0.5f);
        else
            CancelInvoke("FindEnemies");
        SceneManager.LoadScene(sceneName);
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

    public Transform FindRandomEnemyInRange(float range, Transform turret)
    {
        List<Transform> inRange = new List<Transform>();
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                if (Vector3.Distance(turret.position, enemy.transform.position) <= range)
                    inRange.Add(enemy.transform);
            }
        }
        if (inRange.Count != 0)
            return inRange[Random.Range(0, inRange.Count)];
        else
            return null;
    }
}
