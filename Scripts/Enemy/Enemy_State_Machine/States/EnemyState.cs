using System;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;

    public EnemyState(Enemy possessedCharacter, EnemyStateMachine stateMachine)
    {
        this.enemy = possessedCharacter;
        this.stateMachine = stateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void TargetEnteredAggroRange(Player player) { }
    public virtual void HitByBullet() { }
    public virtual void OnAttackAnimationFinished() { }
    public virtual void OnScreamAnimationFinished() { }

}
    

    
