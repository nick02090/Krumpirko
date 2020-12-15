using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Enemy
{
    public class StateWander : EnemyState
    {
        private float startTime;

        public StateWander(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player)
                    : base(_enemy, _agent, _anim, _player)
        {
            name = STATE.WANDER;
            agent.isStopped = false;

            startTime = Time.time;
            
            agent.speed = 2f;
        }

        public override void Enter()
        {
            Debug.Log("I'm in Wander");

            SetRandomDestination(-5f, 5f);

            base.Enter();
        }

        public override void Update()
        {
            if (CanSensePlayer())
            {
                nextState = new StateChase(enemy, agent, anim, player);
                stage = EVENT.EXIT;
            }
            else if (Time.time - startTime < 3f && agent.remainingDistance < 0.1f)
            {
                startTime = Time.time;
                SetRandomDestination(-5f, 5f);
            }
            else if(Time.time - startTime < 3f && Random.Range(0,1000) < 1)
            {
                nextState = new StateIdle(enemy, agent, anim, player);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}