using UnityEngine;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void EnterState()
    {
        player.isSprinting = true;
        player.EnableHeadBobbing?.Invoke(true, true);
     //   Debug.Log("run state entered");
    }

    public override void ExitState()
    {
        // player.isSprinting = false; // player.currentSpeed =
    }

    public override void FrameUpdate()
    {
        player.UpdateMove();
        player.UpdateJumpCooldown();
        

        if (!player.TryRun() && player.TryMove())
        {
            stateMachine.ChangeState(player.walkState);
        }
        else if (player.currentJumpCooldown <= 0 && player.TryJump())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        else if (!player.TryMove())
        {
            stateMachine.ChangeState(player.idleState);
        }
        else if (!player.isGrounded)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
