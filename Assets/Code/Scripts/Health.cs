using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float baseHitPoints = 30;  // Initial HP before scaling
    [SerializeField] private float hitPointsScalingFactor = 1.1f; // Health scaling factor per wave
    [SerializeField] private int currencyWorth = 50;

    private int hitPoints;
    public int damage;

    private void Start()
    {
        // Initialize and scale health based on the current wave from LevelManager
        int currentWave = EnemySpawner.Instance.GetCurrentWave();
        hitPoints = Mathf.RoundToInt(baseHitPoints * Mathf.Pow(hitPointsScalingFactor, currentWave - 1));
        Debug.Log($"{gameObject.name} initialized with {hitPoints} HP for wave {currentWave}");
    }

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;
        Debug.Log($"{gameObject.name} took {dmg} damage, remaining HP: {hitPoints}");

        if (hitPoints <= 0)
        {
            EnemySpawner.onEnemyDestroy.Invoke();  // Just decrements the enemy counter
            LevelManager.Main.IncreaseCurrency(currencyWorth);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)  // Do not check Is "Trigger box" on Collider 2D!
    {
        if (other.gameObject.GetComponent<BaseHealth>() != null)
        {
            other.gameObject.GetComponent<BaseHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}