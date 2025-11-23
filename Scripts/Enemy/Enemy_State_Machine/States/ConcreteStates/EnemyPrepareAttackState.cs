using UnityEngine;
using UnityEngine.AI;

public class EnemyPrepareAttackState : EnemyState
{
    float currentAttackTime = 0;
    public EnemyPrepareAttackState(Enemy possessedCharacter, EnemyStateMachine stateMachine) : base(possessedCharacter, stateMachine)
    {
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
        currentAttackTime = 0;
    }

    public override void FrameUpdate()
    {
        enemy.UpdatePlayerDetachTimer();


        if (enemy.target == null)
        {
            enemy.stateMachine.ChangeState(enemy.idleState);
            return;
        }
        else if ((enemy.target.transform.position - enemy.transform.position).magnitude > enemy.attackRange)
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }
        if(enemy.isScreaming)
        {
            stateMachine.ChangeState(enemy.attackState);
        }

        enemy.navMeshAgent.destination = enemy.target.transform.position;
        enemy.animator.SetFloat("Speed", enemy.navMeshAgent.velocity.magnitude / enemy.navMeshAgent.speed);

        if (currentAttackTime < enemy.attackTimeInterval)
        {
            currentAttackTime += Time.deltaTime;
        }
        else
        {
            currentAttackTime = 0;
            currentAttackTime = 0;

            if (enemy.target == enemy.playerRef)
            {
                enemy.ResetPlayerDetachTimer();
            }

            stateMachine.ChangeState(enemy.attackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void HitByBullet()
    {
        enemy.ResetPlayerDetachTimer();
    }
}
