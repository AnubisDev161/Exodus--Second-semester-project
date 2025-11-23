using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState { get; set; }

    public void Initialize(PlayerState startingState)
    {
        currentState = startingState;
        currentState.EnterState();
    }

    public void ChangeState(PlayerState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }
        currentState = newState;
        currentState.EnterState();
    }



}