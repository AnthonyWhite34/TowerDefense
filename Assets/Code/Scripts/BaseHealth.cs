using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rbBase;


    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

    private void Update()
    {

    }

    private void Start()
    {
        currentHealth = maxHealth; // Initialize base health
    }

    public void TakeDamage(int damage) // function called in Health.
    {
        currentHealth = currentHealth - damage;
        EnemySpawner.onEnemyDestroy.Invoke(); // destroys enemy and adds to the counters.
        Debug.Log($"BaseHealth: {currentHealth}");
        // Check if base health is depleted
        if (currentHealth <= 0)
        {
            // Need to stop the game then call the Loose screen
            // i think im gonn try and just 
            LevelManager.Main.EndGame();
            

        }
    }

    
}
