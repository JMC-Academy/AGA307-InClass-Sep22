using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] spawnPoints;     //The spawn point for our enemies to spawn at
    public GameObject[] enemyTypes;     //Contains all the different enemy types in our game
    public List<GameObject> enemies;    //A list containing all the enemies in our scene
    public int spawnCount = 10;
    public string killCondition = "Two";

    void Start()
    {
        SpawnEnemies();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            SpawnEnemy();
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            KillAllEnemies();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            KillSpecificEnemy(killCondition);
        }
    }

    /// <summary>
    /// Spawns a random enemy at a random spawn point
    /// </summary>
    void SpawnEnemy()
    {
        int enemyNumber = Random.Range(0, enemyTypes.Length);
        int spawnPoint = Random.Range(0, spawnPoints.Length);
        GameObject enemy = Instantiate(enemyTypes[enemyNumber], spawnPoints[spawnPoint].position, spawnPoints[spawnPoint].rotation, transform);
        enemies.Add(enemy);
        print(enemies.Count);
    }

    /// <summary>
    /// This will spawn an enemy at each spawn point sequentially
    /// </summary>
    void SpawnEnemies()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject enemy = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Length)], spawnPoints[i].position, spawnPoints[i].rotation, transform);
            enemies.Add(enemy);
        }
    }

    /// <summary>
    /// Kills a specific enemy in our game
    /// </summary>
    /// <param name="_enemy">The enemy we wish to kill</param>
    void KillEnemy(GameObject _enemy)
    {
        if (enemies.Count == 0)
            return;

        Destroy(_enemy);
        enemies.Remove(_enemy);
    }

    /// <summary>
    /// Kills an enemy of the specified condition
    /// </summary>
    /// <param name="_condition">The condition of the enemy we want to kill</param>
    void KillSpecificEnemy(string _condition)
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].name.Contains(_condition))
                KillEnemy(enemies[i]);
        }
    }

    /// <summary>
    /// Kills all enemies within our scene
    /// </summary>
    void KillAllEnemies()
    {
        if (enemies.Count == 0)
            return;

        for(int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i]);
        }
        enemies.Clear();
    }
}
