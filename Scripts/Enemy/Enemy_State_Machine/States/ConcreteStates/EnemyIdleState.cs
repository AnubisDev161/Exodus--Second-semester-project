using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void EnterState()
    {
        enemy.target = ConvoyAndEnemyNotifier.instance.GetNearestTarget(enemy.gameObject);
        enemy.playerRef = ConvoyAndEnemyNotifier.instance.GetPlayer();
        if (enemy.target == null && enemy.playerRef != null)
        {
            enemy.target = enemy.playerRef.gameObject;
        }
    }

    public override void FrameUpdate()
    {
        enemy.animator.SetFloat("Speed", enemy.navMeshAgent.velocity.magnitude / enemy.navMeshAgent.speed);
        if (enemy.target != null)
        {
            stateMachine.ChangeState(enemy.chaseState);
        }
    }

    public override void HitByBullet()
    {
        //if (enemy.playerRef == null)
        //{
        //    return;
        //}
        //enemy.target = enemy.playerRef.gameObject;
    }

    public override void TargetEnteredAggroRange(Player player)
    {
        if (enemy.target == null || enemy.target != player)
        {
            enemy.playerRef = player;
            enemy.target = player.gameObject;
            enemy.ResetPlayerDetachTimer();
        }
    }
}
