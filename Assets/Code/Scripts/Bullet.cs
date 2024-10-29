using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject); // Destroy bullet if target is lost
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;

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
