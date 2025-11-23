using NUnit.Framework;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBattleState : EnemyState
{
    private Transform player;
    private float lastTimeWasInBattle; 

    public EnemyBattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        UpdateBattleTimer();

        if(player == null)
            player = enemy.GetPlayerReference();

        if(ShouldRetreat())
        {
            rb.linearVelocity = new Vector2(enemy.retreatVelocity.x * -DirectionToPlayer(), enemy.retreatVelocity.y);
            enemy.handleFlip(DirectionToPlayer());
        }
    }

    public override void Update()
    {
        base.Update();

        if(enemy.PlayerDetected())
            UpdateBattleTimer();

        if(BattleTimeIsOver())
            stateMachine.ChangeState(enemy.idleState);

        if(WithinAttackRange() && enemy.PlayerDetected())
            stateMachine.ChangeState(enemy.attackState);
        else
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionToPlayer(), rb.linearVelocity.y);
    }

    private void UpdateBattleTimer() => lastTimeWasInBattle = Time.time;

    private bool BattleTimeIsOver() => Time.time > lastTimeWasInBattle + enemy.battleTimeDuration;

    private bool WithinAttackRange() => DistanceToPlayer() < enemy.attackDistance;

    private bool ShouldRetreat() => DistanceToPlayer() < enemy.minRetreatDistance;

    private float DistanceToPlayer()
    {
        if(player == null)
            return float.MaxValue; // Return a large value if player is not detected

        return Mathf.Abs(player.position.x - enemy.transform.position.x);
    }

    protected int DirectionToPlayer()
    {
        if (player == null)
            return 0; // No movement if player is not detected
    
        float verticalDistance = Mathf.Abs(player.position.y - enemy.transform.position.y);
        float horizontalDistance = Mathf.Abs(player.position.x - enemy.transform.position.x);
    
        // Ignore movement if player is too high and very close horizontally
        if (verticalDistance > 1.5f && horizontalDistance < 1f)
            return 0;
    
        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }
}
