using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWander : EnemyState
{
    public StateWander(GameObject _enemy, UnityEngine.AI.NavMeshAgent _agent, Animator _anim, Transform _player)
                : base(_enemy, _agent, _anim, _player)
    {
        name = STATE.WANDER;
        agent.isStopped = false;
        agent.speed = 1f;
    }

    public override void Enter()
    {
        // Debug.Log("I'm in Wander");

        SetRandomDestination(-5f, 5f);

        base.Enter();
    }

    public override void Update()
    {
        if (CanSeePlayer())
        {
            nextState = new StateChase(enemy, agent, anim, player);
            stage = EVENT.EXIT;
        }
        else if(Random.Range(0,1000) < 1)
        {
            nextState = new StateIdle(enemy, agent, anim, player);
            stage = EVENT.EXIT;
        }
        else if (Random.Range(0, 1000) < 1)
        {
            SetRandomDestination(-5f, 5f);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
