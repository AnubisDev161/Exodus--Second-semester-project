using UnityEngine;

public class NPCState
{
    protected ConvoyNPC convoyNPC;
    protected ConvoyNPCStateMachine stateMachine;

    public NPCState(ConvoyNPC convoyNPC, ConvoyNPCStateMachine stateMachine)
    {
        this.convoyNPC = convoyNPC;
        this.stateMachine = stateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void HitByBullet() { }
    

    
}
    

    
