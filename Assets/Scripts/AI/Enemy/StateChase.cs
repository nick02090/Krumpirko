using UnityEngine;
using UnityEngine.AI;

public class StateChase : EnemyState
{
    public StateChase(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player)
                : base(_enemy, _agent, _anim, _player)
    {
        name = STATE.CHASE;
        agent.speed = 5;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        // Debug.Log("I'm in Chase");

        // anim.SetTrigger("isRunning");
        visAngle = 20f;
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(player.position);
        if(agent.hasPath)
        {
            if (CanStealFries())
            {
                nextState = new StateEating(enemy, agent, anim, player);
                stage = EVENT.EXIT;
            }
            else if (!CanSeePlayer() && Random.Range(0, 100) < 10)
            {
                nextState = new StateIdle(enemy, agent, anim, player);
                stage = EVENT.EXIT;
            }
        }
    }

    public override void Exit()
    {
        // anim.ResetTrigger("isRunning");
        visAngle = 60f;
        base.Exit();
    }
}