using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void EnterState()
    {
        //Debug.Log("player idle state");
        player.EnableHeadBobbing?.Invoke(false, false);
        player.isSprinting = false;

        // reset isDoublejumping only when player is grounded cause idle is also triggered after world switch which can happen in air
        if (player.isGrounded)
        {
            player.isDoublejumping = false;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        player.UpdateJumpCooldown();
        player.UpdateMove();
        player.CheckForInteractableInRange();

        if (!player.isGrounded)
        {
            stateMachine.ChangeState(player.fallState);
        } 
        else if (player.TryRun() && player.TryMove())
        {
            stateMachine.ChangeState(player.runState);
        }
        else if (player.TryMove())
        {
            stateMachine.ChangeState(player.walkState);
        }
        else if (player.currentJumpCooldown <= 0 && player.TryJump())
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
