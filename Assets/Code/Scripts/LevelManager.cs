using System.Diagnostics.CodeAnalysis;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Main;

    [Header("References")]
    public Transform startPoint;
    public Transform[] path;
    [SerializeField] private GameObject basePrefab;

    [Header("Attributes")]
    [SerializeField] public Transform endPoint;
    public int currency;
    public static bool gameOver = false;

    private void Awake()
    {
        Main = this;
    }

    private void Start()
    {
        endPoint = path[path.Length - 1];
        Debug.Log($"end of path is set to {endPoint}");
        currency = 100;
        SpawnBaseHealth();
    }

    public void SpawnBaseHealth()
    {
        

        if (endPoint != null && basePrefab != null)
        {
            Instantiate(basePrefab, endPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("endPoint or baseHealthPrefab is not set.");
        }
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("You do not have enough currency to purchase this item.");
            return false;
        }
    }

    public void LevelWon()
    {
        Debug.Log("LevelWon Called in LevelManager");

        // Stop enemy spawning
        EnemySpawner enemySpawner = gameObject.GetComponent<EnemySpawner>();
        if (enemySpawner != null)
        {
            enemySpawner.StopSpawning();
        }
        else
        {
            Debug.LogError("EnemySpawner component not found on LevelManager GameObject.");
        }

        // Call the Won Screen method from Menu instead
         Menu menu = FindFirstObjectByType<Menu>();
        
            Debug.Log("YouWon called from LevelWon method in LevelManager");
        menu.ShowYouWonScreen(); // shows the you won scree. 
        
        
            //Debug.LogError("Menu component not found in the scene.");
        
    }

    // When called, it will end the game
    public void EndGame()
    {
        Debug.Log("EndGame Called in LevelManager");
        gameOver = true;

        // Stop enemy spawning
        EnemySpawner enemySpawner = gameObject.GetComponent<EnemySpawner>();
        if (enemySpawner != null)
        {
            enemySpawner.StopSpawning();
        }
        else
        {
            Debug.LogError("EnemySpawner component not found on LevelManager GameObject.");
        }

        // Call the ShowGameOverScreen method from Menu instead
        Menu menu = FindFirstObjectByType<Menu>();
        if (menu != null)
        {
            Debug.Log("GameOverScreen called from EndGame method in LevelManager");
            menu.ShowGameOverScreen();
        }
        else
        {
            Debug.LogError("Menu component not found in the scene.");
        }
    }

    public void ForceWin()
    {
        EnemySpawner enemySpawner = gameObject.GetComponent<EnemySpawner>();
        enemySpawner.ForceMaxKills();
    }
}
