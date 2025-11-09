using UnityEngine;

public class PlayerDashState : EntityState
{
    private float originalGravityScale;
    private int dashDirection;

    public PlayerDashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;
        dashDirection = player.moveInput.x != 0 ? (int)player.moveInput.x : player.facingDirection;

        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0f;
    }

    public override void Update()
    {
        base.Update();

        CancelDash();

        player.SetVelocity(player.dashSpeed * dashDirection, 0f);

        // Implement dash logic here
        if (stateTimer < 0f)
        {
            if (player.groundDetected)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.fallState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0f, 0f);
        rb.gravityScale = originalGravityScale;
    }

    private void CancelDash()
    {
        if (player.wallDetected)
        {
            if(player.groundDetected)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.wallSlideState);
            }
        }
    }
}
