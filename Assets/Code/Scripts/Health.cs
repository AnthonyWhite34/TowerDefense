using UnityEngine;


public class Health : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;

    public int damage;
    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;
        Debug.Log($"{gameObject.name} took {dmg} damage, remaining HP: {hitPoints}");

        if (hitPoints <= 0)
        {
            EnemySpawner.onEnemyDestroy.Invoke();//justdecrements the enemy counter
            //EnemySpawner.FindFirstObjectByType<EnemySpawner>().EnemyDestroyed();
            LevelManager.Main.IncreaseCurrency(currencyWorth);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)//Do not check Is "Trigger box" on Collider 2D! When enemy runs into BaseHealth
    {
        if (other.gameObject.GetComponent<BaseHealth>() != null)
        {
            other.gameObject.GetComponent<BaseHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
