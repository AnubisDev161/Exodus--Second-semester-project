 using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {

    }

    public override void EnterState()
    {
        player.EnableHeadBobbing?.Invoke(true, false);
        player.isSprinting = false;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        player.UpdateMove();
        player.UpdateJumpCooldown();
        player.CheckForInteractableInRange();

        if (player.TryRun() && player.TryMove())
        {
            stateMachine.ChangeState(player.runState);
        }
        else if (player.currentJumpCooldown <= 0 && player.TryJump())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        else if (!player.TryMove())
        {
            stateMachine.ChangeState(player.idleState);
        }
        else if(!player.isGrounded)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
