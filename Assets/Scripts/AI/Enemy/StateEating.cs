using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEating : EnemyState
{

    private float startTime;

    public StateEating(GameObject _enemy, UnityEngine.AI.NavMeshAgent _agent, Animator _anim, Transform _player)
                : base(_enemy, _agent, _anim, _player)
    {
        name = STATE.EATING;
        agent.isStopped = false;
        agent.speed = 0.5f;

        startTime = Time.time;
    }

    public override void Enter()
    {
        Debug.Log("I'm in Eating");

        // anim.SetTrigger("isEating");
        
        SetRandomDestination(-2.5f, 2.5f);

        base.Enter();
    }

    public override void Update()
    {
        if (Time.time - startTime > 5)
        {
            nextState = new StateIdle(enemy, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        // anim.ResetTrigger("isEating");
        base.Exit();
    }
}
