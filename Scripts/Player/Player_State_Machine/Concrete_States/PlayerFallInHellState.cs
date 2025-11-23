using System;
using System.Collections.Generic;
using UnityEngine;
public class PlayerFallInHellState : PlayerState
{
    private float inAirTime;
    public PlayerFallInHellState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void EnterState()
    {
        //Debug.Log("Enter fall in hell state");
        player.EnableHeadBobbing?.Invoke(false, false);
    }

    public override void ExitState()
    {
    }

    public override void FrameUpdate()
    {
        player.UpdateMove();
        inAirTime += Time.deltaTime;

        if (player.isGrounded)
        {
            if (inAirTime >= player.fallingEffectTriggertime)
            {
                player.TriggerFallingEffect?.Invoke();
            }
            inAirTime = 0;
            stateMachine.ChangeState(player.idleInHellState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}

