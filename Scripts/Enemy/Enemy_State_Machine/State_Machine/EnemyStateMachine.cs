using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState {  get; set; }

    public void Initialize(EnemyState startingState)
    {
        currentState = startingState;
        currentState.EnterState();
    }

    public void ChangeState(EnemyState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }


   
}
