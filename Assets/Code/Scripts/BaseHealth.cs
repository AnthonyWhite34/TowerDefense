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

    public void TakeDamage(int damage) // function called in EnemyMovement
    {
        currentHealth = currentHealth - damage;
        Debug.Log($"BaseHealth: {currentHealth}");
        // Check if base health is depleted
    }

    
}
