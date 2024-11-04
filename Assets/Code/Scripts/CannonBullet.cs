using UnityEngine;

public class CannonBullet : Bullet
{
    [Header("Cannon Bullet Attributes")]
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private int explosionDamage = 3;

    protected override void OnCollisionEnter2D(Collision2D other)
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
