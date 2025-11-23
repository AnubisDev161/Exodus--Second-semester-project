using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void EnterState()
    {
         enemy.navMeshAgent.speed = enemy.speed;
    }

    public override void ExitState()
    {
        enemy.ResetPlayerDetachTimer();
    }

    public override void FrameUpdate()
    {
         enemy.UpdatePlayerDetachTimer();
        if (enemy.target == null)
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }

        enemy.navMeshAgent.destination = enemy.target.transform.position;
        enemy.animator.SetFloat("Speed", enemy.navMeshAgent.velocity.magnitude / enemy.navMeshAgent.speed);


        if ((enemy.target.transform.position - enemy.transform.position).magnitude <= enemy.attackRange)
        {
            stateMachine.ChangeState(enemy.prepareAttackState);
        }
    }

    public override void HitByBullet()
    {
        enemy.ResetPlayerDetachTimer();
        enemy.target = enemy.playerRef.gameObject;
    }

    public override void TargetEnteredAggroRange(Player player)
    {
        if (enemy.target != player)
        {
            enemy.playerRef = player;
            enemy.target = player.gameObject;
            enemy.ResetPlayerDetachTimer();
        }
    }
}
