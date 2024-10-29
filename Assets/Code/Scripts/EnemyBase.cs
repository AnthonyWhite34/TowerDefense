using System.Collections;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [Header("Base Attributes")]
    public float baseSpeed = 2f;

    protected EnemyMovement enemyMovement; 

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.SetMovementSpeed(baseSpeed);
        }
    }

    public void ApplySlow(float slowAmount, float duration)
    {
        if (enemyMovement != null)
        {
            // Reduce movement speed based on the slow amount
            enemyMovement.SetMovementSpeed(baseSpeed * (1 - slowAmount));
            StopCoroutine("ResetSpeedAfterDelay"); // Stop any ongoing slow effect
            StartCoroutine(ResetSpeedAfterDelay(duration)); // Start a new slow effect
        }
    }

    
    // Coroutine to reset speed after slow effect duration
    private IEnumerator ResetSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (enemyMovement != null)
        {
            enemyMovement.SetMovementSpeed(baseSpeed); // Reset to original speed
        }
    }
}
// Delete class if it doesnt work . 
