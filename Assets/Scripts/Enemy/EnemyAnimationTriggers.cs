using System.Xml.Serialization;
using UnityEngine;

public class EnemyAnimationTriggers : EntityAnimationTriggers
{
    private Enemy enemy;
    private EnemyVFX enemyVfx;

    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponentInParent<Enemy>();
        enemyVfx = GetComponentInParent<EnemyVFX>();
    }

    private void EnableCounterWindow()
    {
        enemyVfx.EnableAttackAlert(true);
        enemy.EnableCounterWindow(true);
    }

    private void DisableCounterWindow()
    {
        enemyVfx.EnableAttackAlert(false);
        enemy.EnableCounterWindow(false);
    }
}
