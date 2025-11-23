using UnityEngine;

public class PlayerIdleInHellState : PlayerState
{
    public PlayerIdleInHellState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void EnterState()
    {
       // Debug.Log("player idle state");
        player.EnableHeadBobbing?.Invoke(false, false);
        player.isSprinting = false;
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
            stateMachine.ChangeState(player.fallInHellState);
        }
        else if (player.TryMove())
        {
            stateMachine.ChangeState(player.walkInHellState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
