using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    OneHand, TwoHand, Archer
}
public enum PatrolType
{
    Linear, Random, Loop
}

public class EnemyManager : Singleton<EnemyManager>
{
    public Transform[] spawnPoints;     //The spawn point for our enemies to spawn at
    public GameObject[] enemyTypes;     //Contains all the different enemy types in our game
    public List<GameObject> enemies;    //A list containing all the enemies in our scene
    public int spawnCount = 10;
    public string killCondition = "Two";
    public float spawnDelay = 2f;


    void Start()
    {
        //StartCoroutine(SpawnDelayed());
        ShuffleList(enemies);
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
    /// Spawns an enemy with a delay until enemy count is reached
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnDelayed()
    {
        yield return new WaitForSeconds(spawnDelay);

        //if (_GM.gameState == GameState.Playing)
        //{
            SpawnEnemy();
        //}

        if (enemies.Count <= spawnCount)
        {
            StartCoroutine(SpawnDelayed());
        }
    }

    /// <summary>
    /// Spawns a random enemy at a random spawn point
    /// </summary>
    void SpawnEnemy()
    {
        int enemyNumber = Random.Range(0, enemyTypes.Length);
        int spawnPoint = Random.Range(0, spawnPoints.Length);
        GameObject enemy = Instantiate(enemyTypes[enemyNumber], spawnPoints[spawnPoint].position, transform.rotation, transform);
        enemies.Add(enemy);
        _UI.UpdateEnemyCount(enemies.Count);
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
    public void KillEnemy(GameObject _enemy)
    {
        if (enemies.Count == 0)
            return;

        Destroy(_enemy);
        enemies.Remove(_enemy);
        _UI.UpdateEnemyCount(enemies.Count);
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

    /// <summary>
    /// Gets a random spawn point from the list
    /// </summary>
    /// <returns>A random spawn point</returns>
    public Transform GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }


    void OnGameStateChanged(GameState _gameState)
    {
        switch(_gameState)
        {
            case GameState.Playing:
                StartCoroutine(SpawnDelayed());
                break;
            default:
                StopAllCoroutines();
                break;
        }
    }

    private void OnEnable()
    {
        GameEvents.OnEnemyDie += KillEnemy;
        GameEvents.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyDie -= KillEnemy;
        GameEvents.OnGameStateChanged -= OnGameStateChanged;
    }
}
