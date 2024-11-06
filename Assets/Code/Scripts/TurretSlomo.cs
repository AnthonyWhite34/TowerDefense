using System.Collections;
using System.Xml.Serialization;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEditor;

public class TurretSlomo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 3f;
    [SerializeField] private float aps = .5f;
    [SerializeField] private float freezeTime = 1f;

    private float timeUntilFire;

    private void Update()
    {
            timeUntilFire += Time.deltaTime;
            if (timeUntilFire >= 1f / aps)
            {
            FreezeEnemies();
                timeUntilFire = 0f;
            }
        
    }

    private void FreezeEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        if (hits.Length > 0 )
        {
            for(int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                em.SetMovementSpeed(0.5f);

                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }
    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(freezeTime);

        em.ResetSpeed();
    }
    private void OnDrawGizmosSelected() // draws the range circle. 
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, targetingRange);
    }
}
