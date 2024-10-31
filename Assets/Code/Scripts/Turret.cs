using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] protected Transform firingPoint;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float targetUpdateDelay = 0.1f; // Delay in seconds between target checks
    [SerializeField] private float bps = 1f;



    protected Transform target;
    private Coroutine targetUpdateCoroutine;
    private float timeUntilFire;

    void Start()
    {
        // Start the target update coroutine
        targetUpdateCoroutine = StartCoroutine(UpdateTargetWithDelay());
    }

    void Update()
    {
        // Rotate towards target if it's available
        if (target != null)
        {
            RotateTowardsTarget();
        }
        if (CheckTargetIsInRange())
        {
            timeUntilFire += Time.deltaTime; 
            if(timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }
    
    protected virtual void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }
    // Coroutine to check for targets at regular intervals
    private IEnumerator UpdateTargetWithDelay()
    {
        while (true)
        {
            if (target == null || !CheckTargetIsInRange())
            {
                FindTarget();
            }

            yield return new WaitForSeconds(targetUpdateDelay);
        }
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        if (hits.Length == 0)
        {
            target = null; // Reset target if no enemies are within range
            return;
        }

        float closestDistance = targetingRange;
        Transform closestTarget = null;

        foreach (var hit in hits)
        {
            float distance = Vector2.Distance(transform.position, hit.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = hit.transform;
            }
        }

        target = closestTarget;
    }

    private bool CheckTargetIsInRange()
    {
        if (target == null) return false;
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()// draws the range cicle
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, targetingRange);
    }
}
