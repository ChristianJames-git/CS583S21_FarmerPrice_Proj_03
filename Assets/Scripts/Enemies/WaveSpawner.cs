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
        private string enemy;   //what enemy to spawn
        private float timeToSpawn; //at what time in the level the enemy will spawn

        //constuctor
        public SpawnContainer(float timeSpawn, float spawnClock, float timeAlive, string enemyType)
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

        public string GetEnemyType()
        {
            return enemy;
        }

        public float GetTimeToSpawn()
        {
            return timeToSpawn;
        }

    }

    public List<SpawnContainer> spawns;
    public GameObject flyingEnemy;
    public GameObject groundEnemy;

    private void Awake()
    {
        spawns = new List<SpawnContainer> { };
    }
}
