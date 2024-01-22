using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRadius = 7, time = 1.5f;
    public int numberEnemiesPerSpawn = 0;
    public float spawnIncreaseRate = 60f;
    [SerializeField] GameObject[] enemyTypes;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
        InvokeRepeating("Increment", 30f, spawnIncreaseRate);
}

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemy()
    {
        for(int i = 0; i < numberEnemiesPerSpawn; i++)
        {
            Debug.Log("Spawn");
            Vector2 spawnPos = GameObject.Find("Player").transform.position;
            spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
            Instantiate(enemyTypes[Random.Range(0, 2)], spawnPos, Quaternion.identity);
        }
        yield return new WaitForSeconds(time);
        StartCoroutine(SpawnEnemy());
    }

    private void Increment()
    {
        numberEnemiesPerSpawn = numberEnemiesPerSpawn + 1;
    }
}
