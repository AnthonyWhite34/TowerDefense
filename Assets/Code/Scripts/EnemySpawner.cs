using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 1.25f;
    [SerializeField] private float enemiesPerSecondCap = 0.75f; // don't change

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private int endWave = 5;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps; // Enemies per second
    private bool isSpawning = false;
    private bool gameStopped = false; // Flag to stop all further updates once the game ends

    [Header("List")]
    [SerializeField] private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of EnemySpawner found!");
            Destroy(gameObject);
        }

        //onEnemyDestroy.AddListener(EnemyDestroyed()); // Called from the health class
    }

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentScene.name != "MainMenu")
        {
            StartCoroutine(StartWave());
        }
    }

    void Update()
    {
        if (gameStopped) return; // Stop all updates if the game has ended
        if (!isSpawning) return; // If we're not spawning, skip the update logic

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            //enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        // Check if all enemies have been defeated and no more are left to spawn
        if (enemiesAlive <= 0 && enemiesLeftToSpawn <= 0)
        {

            if (enemiesAlive <= 0 && currentWave == endWave)
            {
                StopSpawning();
                return;
            }
            EndWave();
        }
        //if (enemiesAlive == 0 && currentWave == endWave + 1)
        //{
        //    LevelManager.Main.LevelWon();
        //}
    }

    private void EndWave()
    {
        if (gameStopped) return; // Don't process if the game has ended

        // Check if all enemies are defeated and no more are left to spawn
        if (enemiesAlive <= 0 && enemiesLeftToSpawn <= 0)
        {
            isSpawning = false;
            timeSinceLastSpawn = 0f;

            if (currentWave > endWave)
            {
                StopSpawning();
            }
            else
            {
                currentWave++;
                StartCoroutine(StartWave());
            }
        }
    }

    public void EnemyDestroyed(GameObject enemy)
    {
        if (gameStopped) return; // Ignore enemy destroyed events if the game has ended

        if(spawnedEnemies.Contains(enemy))
        {
            spawnedEnemies.Remove(enemy);
            Destroy(enemy);
            enemiesAlive--;
        }

        spawnedEnemies.RemoveAll(item => item == null);

        enemiesAlive = spawnedEnemies.Count;

        //If all enemies are defeated and we are at the final wave, display the win screen after a delay
        if (enemiesAlive == 0 && currentWave > endWave)
        {
            StartCoroutine(DisplayWinScreenWithDelay(2f)); // 2-second delay, adjust as needed
        }
    }
    private IEnumerator DisplayWinScreenWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        LevelManager.Main.LevelWon();
        gameStopped = true; // Set gameStopped to true to prevent further updates
    }

    public IEnumerator StartWave()
    {
        if (gameStopped) yield break; // Stop wave initiation if the game has ended

        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesAlive = 0;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
    }

    private void SpawnEnemy()
    {
        if (gameStopped) return; // Don't spawn if the game has ended

        //enemiesAlive++;

        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        GameObject enemyInstance = Instantiate(prefabToSpawn, LevelManager.Main.startPoint.position, Quaternion.identity);

        // Add the spawned enemy to the list
        spawnedEnemies.Add(enemyInstance);
        enemiesAlive++;
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
        isSpawning = false; // Stop spawning
        enemiesLeftToSpawn = 0;
        gameStopped = true; // Set the flag to stop further updates

        // If all enemies are defeated, display the win screen after a delay
        if (enemiesAlive <= 0 && currentWave == endWave)
        {
            StartCoroutine(DisplayWinScreenWithDelay(2f));
        }
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }

    // For debug purposes
    public void ForceMaxKills()
    {
        if (gameStopped) return; // Prevent further debug actions if the game has ended

        currentWave = endWave + 1;
        EndWave();
    }
}
