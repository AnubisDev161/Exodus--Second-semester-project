using UnityEngine;
public class PlayerWalkInHellState : PlayerState
{
    public PlayerWalkInHellState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void EnterState()
    {
        player.EnableHeadBobbing?.Invoke(true, false);
      //  Debug.Log("walk in hell state entered");
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

        if (!player.TryMove())
        {
            stateMachine.ChangeState(player.idleInHellState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    
}

