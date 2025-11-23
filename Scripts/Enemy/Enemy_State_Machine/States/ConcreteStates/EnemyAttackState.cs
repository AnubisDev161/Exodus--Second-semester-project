using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy possessedCharacter, EnemyStateMachine stateMachine) : base(possessedCharacter, stateMachine)
    {
    }

    public override void EnterState()
    {
        enemy.animator.SetFloat("Speed", 0);

        if (enemy.isScreaming)
        {
            enemy.animator.SetTrigger("Scream");
        }
        else
        {
            StartRandomAttackAnimation();
        }

        enemy.navMeshAgent.isStopped = true;
       
    }

    public override void ExitState()
    {
        enemy.navMeshAgent.isStopped = false;
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void HitByBullet()
    {
        enemy.ResetPlayerDetachTimer();
    }

    public override void OnAttackAnimationFinished()
    { 
        enemy.isAttacking = false;
        enemy.animator.SetInteger("AttackIndex", 0);
        stateMachine.ChangeState(enemy.prepareAttackState);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void OnScreamAnimationFinished()
    {
        enemy.isScreaming = false;
        stateMachine.ChangeState(enemy.prepareAttackState);
    }

    private void StartRandomAttackAnimation()
    {
        var randomAttackAnimationIndex = UnityEngine.Random.Range(1, 4);
        enemy.animator.SetInteger("AttackIndex", randomAttackAnimationIndex);
        enemy.isAttacking = true;
    }
}
