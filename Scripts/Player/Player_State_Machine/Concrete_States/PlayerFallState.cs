using UnityEngine;
public class PlayerFallState : PlayerState
{
    private float inAirTime;
    public PlayerFallState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void EnterState()
    {
        //Debug.Log("Enter fall state");
        player.EnableHeadBobbing?.Invoke(false, false);
    }

    public override void ExitState()
    {
        
    }

    public override void FrameUpdate()
    {
        inAirTime += Time.deltaTime;

        player.UpdateMove();

        if (player.isGrounded)
        {
            if(inAirTime >= player.fallingEffectTriggertime)
            {
                player.TriggerFallingEffect?.Invoke();
            }
            inAirTime = 0;
            stateMachine.ChangeState(player.idleState);
        }
        else if ((player.currentJumpCooldown <= 0 && player.TryJump()) || player.TryDoubleJump())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}

