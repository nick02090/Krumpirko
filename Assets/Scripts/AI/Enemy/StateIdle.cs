using UnityEngine;
using UnityEngine.AI;

public class StateIdle : EnemyState
{
    public StateIdle(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player)
                : base(_enemy, _agent, _anim, _player)
    {
        name = STATE.IDLE;
        agent.isStopped = true;
    }

    public override void Enter()
    {
        // Debug.Log("I'm in idle");

        // anim.SetTrigger("isIdle"); 
        base.Enter();
    }

    public override void Update()
    {
        if (CanSeePlayer())
        {
            nextState = new StateChase(enemy, agent, anim, player);
            stage = EVENT.EXIT;
        }
        else if(Random.Range(0,1000) < 10)
        {
            nextState = new StateWander(enemy, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        // anim.ResetTrigger("isIdle");
        base.Exit();
    }
}
