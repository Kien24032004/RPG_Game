using System.Xml.Serialization;
using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    private EntityVFX vfx;
    public float damage = 10f;

    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1f;
    [SerializeField] private LayerMask whatIsTarget;

    private void Awake()
    {
        vfx = GetComponent<EntityVFX>();
    }

    public void PerformAttack()
    {
        GetDetectedColliders();

        foreach(var target in GetDetectedColliders())
        {
            IDamgable damgable = target.GetComponent<IDamgable>();

            if(damgable == null)
                continue; // Skip to next target if this target is not damgable 

            damgable.TakeDamage(damage, transform);
            vfx.CreateOnHitVfx(target.transform);

            // EntityHealth targetHealth = target.GetComponent<EntityHealth>();
            // targetHealth?.TakeDamage(damage, transform);
        }
    }

    protected Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
