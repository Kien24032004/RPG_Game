using UnityEngine;

public class PlayerBasicAttackState : PlayerState
{
    private float attackVelocityTimer;
    private float lastTimeAttacked;

    private bool comboAttackQueued;
    private int attackDirection;
    private int comboIndex = 1;
    private int comboLimit = 3;
    private const int FirstComboIndex = 1; // start combo from 1, this value set to animator parameter


    public PlayerBasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        if(comboLimit != player.attackVelocity.Length)
        {
            comboLimit = player.attackVelocity.Length;
        }
    }

    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false;
        ResetComboIndex();
        SyncAttackSpeed();

        // define attack direction according to player input
        attackDirection = player.moveInput.x != 0 ? (int)player.moveInput.x : player.facingDirection;

        anim.SetInteger("basicAttackIndex", comboIndex);
        ApplyAttackVelocity();
    }


    public override void Update()
    {
        base.Update();
        HandleAttackVelocity();

        if(input.Player.Attack.WasPerformedThisFrame())
        {
            QueueNextAttack();
        }

        if (triggerCalled)
        {
            HandleStateExit();
        }
    }

    public override void Exit()
    {
        base.Exit();

        comboIndex++;

        // record last time attacked
        lastTimeAttacked = Time.time;
    }

    private void HandleStateExit()
    {
        if (comboAttackQueued)
        {
            anim.SetBool(animBoolName, false);
            player.EnterAttackStateWithDelay();
        }
        else
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    private void QueueNextAttack()
    {
        comboAttackQueued = true;
    }
    
    // fix the attack velocity not stopping issue
    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer < 0)
        {
            player.SetVelocity(0, rb.linearVelocity.y);
        }
    }

    // set the attack velocity when entering the attack state
    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];

        attackVelocityTimer = player.attackVelocityDuration;
        player.SetVelocity(attackVelocity.x * attackDirection, attackVelocity.y);
    }

    private void ResetComboIndex()
    {
        // reset combo if time exceeded
        if( Time.time > lastTimeAttacked + player.comboRResetTime)
        {
            comboIndex = FirstComboIndex;
        }

        // cycle through combo attacks
        if (comboIndex > comboLimit)
        {
            comboIndex = FirstComboIndex;
        }
    }
}
