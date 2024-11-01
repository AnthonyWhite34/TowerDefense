using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{   
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    private Transform target; // im assuming its the enemy position

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;

        if (target == null)
        {
            Destroy(gameObject); // Destroy bullet if target is lost
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other) //Do not check Is "Trigger box" on Collider 2D!
    {
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage); //original//
        Destroy(gameObject);// Destroy the bullet after impact
    }
}
