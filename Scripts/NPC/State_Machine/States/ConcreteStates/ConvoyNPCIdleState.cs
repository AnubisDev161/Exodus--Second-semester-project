using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using static UnityEngine.GraphicsBuffer;

public class ConvoyNPCIdleState : NPCState
{
    public ConvoyNPCIdleState(ConvoyNPC convoyNPC, ConvoyNPCStateMachine stateMachine) : base(convoyNPC, stateMachine)
    {
    }

    public override void EnterState()
    {

       
    }

    public void StartTargetDetectionTimer()
    {
        
    }
    
    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
         if (convoyNPC.waypoints.Count > 1)
         {
            stateMachine.ChangeState(convoyNPC.patrolState);
         }
    }

    public override void HitByBullet()
    {
        
    }

}
