using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float movementSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;

    private float baseSpeed;
    public bool IsSlowed { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        baseSpeed = movementSpeed;
        // Ensure the enemy is on the "Enemy" layer
        gameObject.layer = LayerMask.NameToLayer("Enemy");

        target = LevelManager.Main.path[pathIndex];
    }

    void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex >= LevelManager.Main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
            }
            else
            {
                target = LevelManager.Main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * movementSpeed;
        //rb.velocity = direction * movementSpeed;
    }

    private void OnDestroy()
    {
        Debug.Log($"Enemy {gameObject.name} destroyed.");  // Logs when an enemy is destroyed
    }

    public void SetMovementSpeed(float newSpeed) 
    {
        movementSpeed = newSpeed;
    }

    public void ResetSpeed()
    {
        movementSpeed = baseSpeed;
        IsSlowed = false;
    }
}
