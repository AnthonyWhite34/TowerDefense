using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Main;

    [Header("References")]
    [SerializeField] private GameObject Path;
    [SerializeField] private GameObject basePrefab;

    [Header("Attributes")]

    public int currency;
    public static bool gameOver = false;

    [Header("Events")]
    public Transform startPoint;
    public Transform[] path;
    public Transform endPoint;


    private void Awake()
    {
        Main = this;

        // Find the GameObject named "path"
        //GameObject pathObject = GameObject.Find("Path");
        
        Debug.Log("Path found");
        if (Path != null)
        {
            int pathLength = Path.transform.childCount;
            // Get all child transforms of the "path" GameObject
            //int pathLength = pathObject.transform.childCount;
            path = new Transform[pathLength];

            for (int i = 0; i < pathLength; i++)
            {
                path[i] = Path.transform.GetChild(i);
            }

            // Set startPoint to the first element in path, and endPoint to the last
            if (path.Length > 0)
            {
                startPoint = path[0];
                endPoint = path[path.Length - 1];
                Debug.Log($"Start Point: {startPoint}, End Point: {endPoint}");
            }
        }
        else
        {
            Debug.LogError("GameObject named 'path' not found in the scene.");
        }
    }

    private void Start()
    {
        currency = 100;
        StartCoroutine(DelayedSpawnBaseHealth());  // Start the coroutine with a delay
    }

    private IEnumerator DelayedSpawnBaseHealth()
    {
        yield return new WaitForSeconds(0.1f); // Small delay to ensure all objects are initialized
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
            Debug.LogWarning("endPoint or basePrefab is not set.");
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

        EnemySpawner enemySpawner = gameObject.GetComponent<EnemySpawner>();
        if (enemySpawner != null)
        {
            enemySpawner.StopSpawning();
        }
        else
        {
            Debug.LogError("EnemySpawner component not found on LevelManager GameObject.");
        }

        Menu menu = FindFirstObjectByType<Menu>();
        if (menu != null)
        {
            Debug.Log("YouWon called from LevelWon method in LevelManager");
            menu.ShowYouWonScreen();
        }
        else
        {
            Debug.LogError("Menu component not found in the scene.");
        }
    }

    public void EndGame()
    {
        Debug.Log("EndGame Called in LevelManager");
        gameOver = true;

        EnemySpawner enemySpawner = gameObject.GetComponent<EnemySpawner>();
        if (enemySpawner != null)
        {
            enemySpawner.StopSpawning();
        }
        else
        {
            Debug.LogError("EnemySpawner component not found on LevelManager GameObject.");
        }

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
        if (enemySpawner != null)
        {
            enemySpawner.ForceMaxKills();
        }
        else
        {
            Debug.LogError("EnemySpawner component not found on LevelManager GameObject.");
        }
    }

}