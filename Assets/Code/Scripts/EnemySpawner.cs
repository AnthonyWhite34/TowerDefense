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
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondCap = 15f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private float eps; // enimes per second
    private bool isSpawning = false;

    [Header("List")]
    private List<GameObject> spawnedEnemies = new List<GameObject>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() 
    {
        //baseEnemies = 8;
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "MainMenu")
        {
            StartCoroutine(StartWave());
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        } 
    }
    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }
    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);// called from the health class
    }
    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }
    public IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);// might not need this
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.Main.startPoint.position, Quaternion.identity);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }

    // Test below
    public void StopSpawning()
    {
        Debug.Log("StopSpawning called in EnemySpawner. enemyPrefabs should be set to null"); 
        isSpawning = true; // the game will stop becuase it thinks the enemys are still spawing. 
        enemiesLeftToSpawn = 0;  // This prevents any remaining enemies from spawning
        foreach (GameObject enemy in spawnedEnemies) // this will destroy all the reamining enemys and the game will be put at a stailmate.
        {
            EnemyDestroyed();
        }
    }
}
