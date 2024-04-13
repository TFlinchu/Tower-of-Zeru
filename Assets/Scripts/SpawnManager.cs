using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numOfEnemies;
    public int totalEnemies;
    public int enemiesKilled;
    float spawnRate = 5;
    float spawnEnemy = 1;
    // public EnterDoor test;
    // Start is called before the first frame update
    void Start()
    {
        totalEnemies = numOfEnemies;
        //test.enterAllowed = false;
    }

    // Update is called once per frame
    void Update()
    {
        spawnEnemy -= Time.deltaTime;
        if (spawnEnemy <= 0 && numOfEnemies > 0) {
            spawnEnemy = spawnRate;
            spawnRate *= 0.9f;
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            numOfEnemies--;
        }
        // if (numOfEnemies == 0) {
        //     test.enterAllowed = false;
        // }

        
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
    }
}