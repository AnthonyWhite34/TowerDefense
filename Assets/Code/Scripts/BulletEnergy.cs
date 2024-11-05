using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnergy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 3f;
    [SerializeField] private int bulletDamage = 30;
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private int explosionDamage = 10;

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
        // Perform explosion damage to all enemies within the explosion radius
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider in hitObjects)
        {
            Health enemyHealth = collider.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(explosionDamage);
            }
        }
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        // Call base behavior to destroy the bullet after collision
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the explosion radius in the editor for better visualization
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}