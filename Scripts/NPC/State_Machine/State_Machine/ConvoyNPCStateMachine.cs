using UnityEngine;

public class ConvoyNPCStateMachine
{
    public NPCState currentState {  get; set; }

    public void Initialize(NPCState startingState)
    {
        currentState = startingState;
        currentState.EnterState();
    }

    public void ChangeState(NPCState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
}
