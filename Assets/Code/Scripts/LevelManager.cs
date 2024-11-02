using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Main;

    public Transform startPoint;
    public Transform[] path;

    public int currency;
    public static bool gameOver = false;

    private Menu menu;

    [SerializeField] private GameObject shopMenu; // Assign in Inspector
    [SerializeField] private GameObject gameOverScreen; // Assign in Inspector

    private void Awake()
    {
        Main = this;

        // Check for an existing Menu component on the same GameObject
        menu = gameObject.GetComponent<Menu>();
        if (menu == null)
        {
            Debug.LogWarning("Menu component not found on this GameObject.");
        }
    }

    private void Start()
    {
        currency = 100;

        // Show shopMenu when the level starts
        if (shopMenu != null)
        {
            shopMenu.SetActive(true);
        }
        else
        {
            Debug.LogError("Shop menu is not assigned in the Inspector.");
        }

        // Ensure Game Over screen is hidden at the start
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
        else
        {
            Debug.LogError("Game Over screen is not assigned in the Inspector.");
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

        // Hide shopMenu and show Game Over screen
        if (shopMenu != null) shopMenu.SetActive(false);
        if (gameOverScreen != null) gameOverScreen.SetActive(true);
    }
}
