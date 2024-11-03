using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 1.25f;
    [SerializeField] private float enemiesPerSecondCap = 15f;

    [Header("Enemy Limits")]
    [SerializeField] private int[] maxEnemiesDefeatedPerLevel = new int[5]; // adds how many enemies to fight each level
    // skip first index because thats the MainMenu. 
    private int maxEnemiesDefeated;
    private int enemiesDefeated = 0;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps; // Enemies per second
    private bool isSpawning = false;


    [Header("List")]
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed); // Called from the health class
    }

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentScene.name != "MainMenu")
        {
            GenerateLevelDifficulty(maxEnemiesDefeatedPerLevel);
            maxEnemiesDefeated = maxEnemiesDefeatedPerLevel[sceneIndex];
            //SetMaxEnemiesDefeated();
            StartCoroutine(StartWave());
        }
    }

    void Update()
    {
        if (!isSpawning) return; //if were already spawning dont spawn
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0) // 30 >= 1 && 
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }
        if(timeSinceLastSpawn >= 5f && enemiesLeftToSpawn <= 0 && enemiesAlive <= 0)
        {
            enemiesLeftToSpawn = 0;
            enemiesAlive = 0;
        }

        // Check if all enemies have been defeated for the wave or if the max defeated limit is reached
        if ((enemiesAlive == 0 && enemiesLeftToSpawn == 0) || enemiesDefeated >= maxEnemiesDefeated)
        {
            EndWave();
        }
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;

        if (enemiesDefeated >= maxEnemiesDefeated)
        {
            Debug.Log("Max enemies defeated for this level. Moving to the next level.");
            StopSpawning();
            LevelManager.Main.LevelWon();// called on level manager to show you win UI
            
        }

        StartCoroutine(StartWave());
    }

    //private void SetMaxEnemiesDefeated()
    //{
    //    // Set max defeated limit based on the level or keep the highest limit if levels exceed array length
    //    maxEnemiesDefeated = currentWave - 1 < maxEnemiesDefeatedPerLevel.Length
    //        ? maxEnemiesDefeatedPerLevel[currentWave - 1]
    //        : maxEnemiesDefeatedPerLevel[maxEnemiesDefeatedPerLevel.Length - 1];
    //}

    public void EnemyDestroyed()
    {
        enemiesAlive--;
        enemiesDefeated++;

        // Check if the max defeated limit has been reached to stop spawning early
        if (enemiesDefeated >= maxEnemiesDefeated)
        {
            StopSpawning();
            LevelManager.Main.LevelWon();// called on level manager to show you win UI
        }
    }

    public IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        GameObject enemyInstance = Instantiate(prefabToSpawn, LevelManager.Main.startPoint.position, Quaternion.identity);

        // Add the spawned enemy to the list
        spawnedEnemies.Add(enemyInstance);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }

    public void StopSpawning()
    {
        Debug.Log("Stopping spawning and destroying all remaining enemies.");
        isSpawning = false;
        enemiesLeftToSpawn = 0;

        // Destroy each spawned enemy and clear the list
        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        spawnedEnemies.Clear(); // Clear the list after destroying all instances
        //LevelManager.Main.EndGame();
    }

    //function that will increase the difficulty and scale enemy size each level. 
    public int[] GenerateLevelDifficulty(int[] array)
    {
        if (array.Length > 1)
        {
            array[1] = 20;
        }

        float growthFactor = 1.25f;


        for (int i = 2; i < array.Length; i++)
        {
            array[i] = Mathf.RoundToInt(array[i - 1] * growthFactor);
        }
        return array;
    }

    public void ForceMaxKills()
    {
        enemiesDefeated = maxEnemiesDefeated;
        EndWave();
    }

}
