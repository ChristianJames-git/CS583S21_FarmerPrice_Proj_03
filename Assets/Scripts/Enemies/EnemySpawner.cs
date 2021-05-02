using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    private Transform spawnLocation;
    public float spawnTimer;
    public float lifeTime;

    private void Start()
    {
        spawnLocation = GameObject.Find("Start").GetComponent<Transform>();
    }

    IEnumerator Spawn()
    {
        //keep spawning until this object is gone
        while(true)
        {
            GameObject.Instantiate(enemyPrefab, spawnLocation.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnTimer);
        }

        //check if this spawber has exceeded it's lifetime
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
