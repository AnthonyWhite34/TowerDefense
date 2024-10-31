using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnergyTurret : Turret
{
    [Header("References")]
    [SerializeField] private GameObject energyBulletPrefab;

    //private Transform target;


    protected override void Shoot()
    {
        
        // Instantiate BulletEnergy prefab instead of the regular bullet
        GameObject bulletObj = Instantiate(energyBulletPrefab, firingPoint.position, Quaternion.identity);
        BulletEnergy bulletScript = bulletObj.GetComponent<BulletEnergy>();
        bulletScript.SetTarget(target);
    }
}
