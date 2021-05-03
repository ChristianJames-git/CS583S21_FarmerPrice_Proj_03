using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnLocation;
    public float spawnTimer;
    public float lifeTime;

    public void Start()
    {
        spawnLocation = transform;
    }

    IEnumerator Spawn()
    {
        //keep spawning until this object is gone
        while(true)
        {
            GameObject.Instantiate(enemyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    private void Update()
    {
        //check if this spawner has exceeded it's lifetime
        if (lifeTime < 0)
            Destroy(this.gameObject);
        else
            lifeTime -= Time.deltaTime;
    }

    //method used to update settings and start spawning sequence during runtime to work with WaveSpawner
    public void SetSettings(float spawnClock, float timeAlive, GameObject enemyType)
    {
        //update settings
        spawnTimer = spawnClock;
        lifeTime = timeAlive;
        enemyPrefab = enemyType;

        //start spawning
        StartCoroutine(Spawn());
    }
}
