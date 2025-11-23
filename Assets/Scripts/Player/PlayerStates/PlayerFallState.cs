using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {

    }
    
    public override void Update()
    {
        base.Update();

        //if player detecting ground, switch to idle state
        if (player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
            player.SetVelocity(0, rb.linearVelocity.y);
        }

        if (player.wallDetected)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }
}
