using UnityEngine;

public class BulletEnergy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 15f;
    [SerializeField] private int bulletDamage = 4;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        // Check if the target is still valid
        if (target == null)
        {
            Destroy(gameObject); // Destroy the bullet if the target is gone
            return;
        }

        // Calculate direction to the target
        Vector2 direction = (target.position - transform.position).normalized;

        // Update the bullet's position with Rigidbody2D's velocity to move toward the target
        rb.linearVelocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Health enemyHealth = other.gameObject.GetComponent<Health>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(bulletDamage); // Apply damage to the enemy
        }

        // Destroy the bullet after impact
        Destroy(gameObject);
    }
}