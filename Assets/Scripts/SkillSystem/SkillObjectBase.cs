using System.Threading;
using UnityEngine;

public class SkillObjectBase : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] protected Transform targetCheck;
    [SerializeField] protected float checkRadius = 1f;

    protected EntityStats playerStats;
    protected DamageScaleData damageScaleData;
    protected ElementType usedElement;

    protected void DamageEnemiesInRadius(Transform transform, float radius)
    {
        foreach(var target in EnemiesAround(transform, radius))
        {
            IDamgable damgable = target.GetComponent<IDamgable>();

            if(damgable == null)
                continue;

            AttackData attackData = playerStats.GetAttackData(damageScaleData);
            EntityStatusHandler statusHandler = target.GetComponent<EntityStatusHandler>();

            float physicalDamage = attackData.physicalDamage;
            float elementalDamage = attackData.elementalDamage;
            ElementType element = attackData.element;

            damgable.TakeDamage(physicalDamage, elementalDamage, element, transform);

            if(element != ElementType.None)
                statusHandler?.ApplyStatusEffect(element, attackData.effectData);
            
            usedElement = element;
        }
    }

    protected Transform FindClosestTarget()
    {
        Transform target = null;
        float closestDistance = Mathf.Infinity;

        foreach(var enemy in EnemiesAround(transform, 10f))
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if(distance < closestDistance)
            {
                target = enemy.transform;
                closestDistance = distance;
            }
        }

        return target;
    }

    protected Collider2D[] EnemiesAround(Transform transform, float radius)
    {
        return Physics2D.OverlapCircleAll(transform.position, radius, whatIsEnemy);
    }

    protected virtual void OnDrawGizmos()
    {
        if(targetCheck == null)
            targetCheck = transform;

        Gizmos.DrawWireSphere(targetCheck.position, checkRadius);
    }
}
