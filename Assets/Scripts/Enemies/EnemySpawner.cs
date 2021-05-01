using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnLocation;
    public float spawnTimer;
    public float lifeTime;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        //keep spawning until this object is gone
        while(true)
        {
            GameObject.Instantiate(enemyPrefab, spawnLocation.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    private void Update()
    {
        if (lifeTime < 0)
            Destroy(this.gameObject);
        else
            lifeTime -= Time.deltaTime;
    }
}
