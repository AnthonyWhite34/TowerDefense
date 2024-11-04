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
