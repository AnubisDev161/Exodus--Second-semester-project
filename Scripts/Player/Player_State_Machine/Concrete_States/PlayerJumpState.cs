using UnityEngine;

public class PlayerJumpState : PlayerState
{
    float groundCheckDelay = 0.1f;
    float currentGroundCheckDelay;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void EnterState()
    {
        player.EnableHeadBobbing?.Invoke(false, false);
        //Debug.Log("player entered jump state");

     
        if(player.hasTriggeredJumpingPad)
        {
            player.currentDoubleJumpCooldown = player.doubleJumpCooldown;
        }
        else if (player.isGrounded || player.leftGroundtime <= player.coyoteTime)
        {
            PerformJump();
        }
        else if(!player.isGrounded && !player.isDoublejumping)
        {
            PerformDoubleJump();
        }
    }

    public override void ExitState()
    {
        currentGroundCheckDelay = 0;
        player.hasTriggeredJumpingPad = false;
    }

    public override void FrameUpdate()
    {
        player.UpdateMove();
        
        if (!player.isDoublejumping)
        {
            if (player.TryDoubleJump())
            {
                PerformDoubleJump();
            }
        }
        currentGroundCheckDelay += Time.deltaTime;

        if(player.velocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void PerformJump()
    {
        player.velocity.y = Mathf.Sqrt(player.doubleJumpHeight * -2f * player.gravity);
        player.currentDoubleJumpCooldown = player.doubleJumpCooldown;
    }

    private void PerformDoubleJump()
    {
        player.velocity.y = Mathf.Sqrt(player.jumpHeight * -2f * player.gravity);
        player.currentJumpCooldown = player.jumpCooldown;
        player.isDoublejumping = true;
    }
}
