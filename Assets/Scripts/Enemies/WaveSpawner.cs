using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    //class for storing details about what to spawn
    public class SpawnContainer
    { 

        private float spawnTimer;   //how often to spawn
        private float lifeTime; //how long to spawn for
        private GameObject enemy;   //what enemy to spawn
        private float timeToSpawn; //at what time in the level the enemy will spawn

        //constuctor
        public SpawnContainer(float timeSpawn, float spawnClock, float timeAlive, GameObject enemyType)
        {
            timeToSpawn = timeSpawn;
            spawnTimer = spawnClock;
            lifeTime = timeAlive;
            enemy = enemyType;
        }

        //getter methods
        public float getSpawnTimer()
        {
            return spawnTimer;
        }

        public float GetLifeTime()
        {
            return lifeTime;
        }

        public GameObject GetEnemyType()
        {
            return enemy;
        }

        public float GetTimeToSpawn()
        {
            return timeToSpawn;
        }

    }

    public List<SpawnContainer> waves;
    public GameObject spawner;
    private float timer;
    public GameObject flyingEnemy;
    public GameObject groundEnemy;

    private void Awake()
    {
        waves = new List<SpawnContainer> {new SpawnContainer(1, 2, 10, flyingEnemy),
                                          new SpawnContainer(5, 1, 10, groundEnemy)};

        //start timer at 0
        timer = 0;
    }

    private void Update()
    {
        //set timer
        timer += Time.deltaTime * Time.timeScale;

        if (waves.Count > 0)
        {
            EnemySpawner temp;

            //check is the next wave should spawn
            if (waves[0].GetTimeToSpawn() <= timer)
            {
                //spawn that enemy spawner
                temp = Transform.Instantiate(spawner, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<EnemySpawner>();
                temp.SetSettings(waves[0].getSpawnTimer(), waves[0].GetLifeTime(), waves[0].GetEnemyType());

                //remove it form the list, we have spawned the wave
                waves.RemoveAt(0);
            }
        }
        else
        {
            //we dont have anymore waves to spawn
            Destroy(this.gameObject);
        }

    }
}
