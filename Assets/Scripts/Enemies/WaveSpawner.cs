using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    //class for storing details about what to spawn
    private class SpawnContainer
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

    private class Wave {
        private List<SpawnContainer> waveEnemies;

        public Wave(List<SpawnContainer> enemies)
        {
            waveEnemies = enemies;
        }

        public List<SpawnContainer> GetWave()
        {
            return waveEnemies;
        }
    }

    private List<Wave> waves;
    public GameObject spawner;
    private float timer;
    private float waveEndTime;
    public GameObject flyingEnemy;
    public GameObject groundEnemy;
    private int currWave;
    public TMP_Text waveDisplay;
    private bool inWave;

    private void Awake()
    {
        waves = new List<Wave>();
        waves.Add(new Wave(new List<SpawnContainer> { new SpawnContainer(2, 3, 15, groundEnemy) }));
        waves.Add(new Wave(new List<SpawnContainer> { new SpawnContainer(2, 3, 15, flyingEnemy), new SpawnContainer(1, 4, 18, groundEnemy) }));
        Debug.Log(waves.Count);
        //start timer at 0
        timer = 0;
        currWave = 0;
    }

    private void Update()
    {
        //if (!GameManager.Instance.paused)
        //{
            //set timer
            timer += Time.deltaTime * Time.timeScale;

            if (inWave && timer > waveEndTime)
            {
                Debug.Log("Waves removed");
                inWave = false;
                waves.RemoveAt(0);

            }

            if (Input.GetKeyDown(KeyCode.Return) && !inWave)
            {
                Debug.Log("Return pressed");
                if (waves.Count > 0)
                {
                    waveDisplay.text = (++currWave).ToString();
                    //spawn Wave
                    List<SpawnContainer> tempWave = waves[0].GetWave();
                    inWave = true;
                    waveEndTime = timer + SpawnWave(tempWave);
                }
                else
                    Destroy(this.gameObject);
            }
        //}
    }

    private float SpawnWave(List<SpawnContainer> tempWave)
    {
        float longestTime = 0;
        foreach (SpawnContainer container in tempWave) {
            EnemySpawner temp = Instantiate(spawner, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<EnemySpawner>();
            temp.SetSettings(container.getSpawnTimer(), container.GetLifeTime(), container.GetEnemyType());
            if (container.GetLifeTime() > longestTime)
                longestTime = container.GetLifeTime();
        }
        return longestTime;
    }
}
