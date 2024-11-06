using UnityEngine;
using System.Collections;

public class DualFiringTurret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint1;
    [SerializeField] private Transform firingPoint2;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float bps = 1f;

    private bool useFirstFiringPoint = true; // Track which firing point to use

    protected Transform target;
    private float timeUntilFire;

    private void Start()
    {
        FindTarget();
    }

    void Update()
    {
        // Check if the current target is still valid
        if (target != null && !CheckTargetIsInRange())
        {
            target = null; // Reset target if it's out of range
        }

        // Find a new target only if there's no current target
        if (target == null)
        {
            FindTarget();
        }

        // Rotate towards and shoot at the target if available
        if (target != null)
        {
            RotateTowardsTarget();
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
        // Determine which firing point to use
        Transform selectedFiringPoint = useFirstFiringPoint ? firingPoint1 : firingPoint2;

        // Instantiate the bullet at the selected firing point
        GameObject bulletObj = Instantiate(bulletPrefab, selectedFiringPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);

        // Toggle the firing point for the next shot
        useFirstFiringPoint = !useFirstFiringPoint;
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

    private void OnDrawGizmosSelected() // Draws the range circle
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, targetingRange);
    }
}


/*
using UnityEngine;

public class DualFiringTurret : Turret
{
    [Header("Double Barrel Attributes")]
    [SerializeField] private Transform firingPoint1;
    [SerializeField] private Transform firingPoint2;

    private bool useFirstFiringPoint = true; // Track which firing point to use

    protected override void Shoot()
    {
        // Determine which firing point to use
        Transform selectedFiringPoint = useFirstFiringPoint ? firingPoint1 : firingPoint2;

        // Instantiate the bullet at the selected firing point
        GameObject bulletObj = Instantiate(bulletPrefab, selectedFiringPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);

        // Toggle the firing point for the next shot
        useFirstFiringPoint = !useFirstFiringPoint;
    }
}
*/