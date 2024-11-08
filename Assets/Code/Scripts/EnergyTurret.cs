using UnityEngine;

public class EnergyTurret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject energyBulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 8f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float bps = 0.5f; // Shots per second

    private Transform target;
    private float timeUntilFire;

    void Start()
    {
        // Immediately look for an initial target at start
        FindTarget();
    }

    void Update()
    {
        // Continuously look for a new target if none exists or target is out of range
        if (target == null || !CheckTargetIsInRange())
        {
            FindTarget();
        }

        // Rotate towards target if it's available
        if (target != null)
        {
            RotateTowardsTarget();
        }

        // Check if target is in range and shoot if ready
        if (CheckTargetIsInRange())
        {
            timeUntilFire += Time.deltaTime;
            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        // Instantiate BulletEnergy prefab at the firing point
        GameObject bulletObj = Instantiate(energyBulletPrefab, firingPoint.position, Quaternion.identity);
        BulletEnergy bulletScript = bulletObj.GetComponent<BulletEnergy>();
        bulletScript.SetTarget(target);
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
