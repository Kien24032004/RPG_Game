using System.Xml.Serialization;
using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    private EntityVFX vfx;
    private EntityStats stats;

    public DamageScaleData basicAttackScale;

    [Header("Target Detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1f;
    [SerializeField] private LayerMask whatIsTarget;


    private void Awake()
    {
        vfx = GetComponent<EntityVFX>();
        stats = GetComponent<EntityStats>();
    }

    public void PerformAttack()
    {
        GetDetectedColliders();

        foreach(var target in GetDetectedColliders())
        {
            IDamgable damgable = target.GetComponent<IDamgable>();

            if(damgable == null)
                continue; // Skip to next target if this target is not damgable 

            AttackData attackData = stats.GetAttackData(basicAttackScale);
            EntityStatusHandler statusHandler = target.GetComponent<EntityStatusHandler>();

            float physicalDamage = attackData.physicalDamage;
            float elementalDamage = attackData.elementalDamage;
            ElementType element = attackData.element;

            bool targetGotHit = damgable.TakeDamage(physicalDamage, elementalDamage, element, transform);

            if(element != ElementType.None)
                statusHandler?.ApplyStatusEffect(element, attackData.effectData);

            if(targetGotHit)
                vfx.CreateOnHitVfx(target.transform, attackData.isCrit, element);
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
