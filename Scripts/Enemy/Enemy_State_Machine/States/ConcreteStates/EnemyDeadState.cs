using UnityEngine;

public class EnemyDeadState : EnemyState
{

    private bool playOnce = false;
    public EnemyDeadState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void EnterState()
    {
        if (!playOnce)
        {
            Enemy.OnEnemyDied?.Invoke();
            enemy.EnableHitBox(false);
            enemy.animator.SetTrigger("Dead");
            enemy.navMeshAgent.isStopped = true;
            enemy.isDead = true;
            playOnce = true;
        }
    }
}
