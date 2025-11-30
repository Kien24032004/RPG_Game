using UnityEngine;

public class EnemySkeleton : Enemy , ICounterable
{
    public bool CanbeCountered { get => canBeStunned; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new EnemyIdleState(this, stateMachine, "idle");
        moveState = new EnemyMoveState(this, stateMachine, "move");
        attackState = new EnemyAttackState(this, stateMachine, "attack");
        battleState = new EnemyBattleState(this, stateMachine, "battle");
        deadState = new EnemyDeadState(this, stateMachine, "idle");
        stunnedState = new EnemyStunnedState(this, stateMachine, "stunned");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    public void HandleCounter()
    {
        if(CanbeCountered == false)
            return;

        stateMachine.ChangeState(stunnedState);
    }
}
