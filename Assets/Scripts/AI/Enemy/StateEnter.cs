using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Enemy
{
    public class StateEnter : EnemyState
    {
        private float startTime;

        public StateEnter(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player)
                    : base(_enemy, _agent, _anim, _player)
        {
            name = STATE.ENTER;
            agent.isStopped = false;

            startTime = Time.time;

            agent.speed = 2f;
        }

        public override void Enter()
        {
            Debug.Log("I'm in Enter");

            Vector3 randomVector = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
            agent.SetDestination(randomVector); 

            base.Enter();
        }

        public override void Update()
        {
            if (CanSensePlayer())
            {
                nextState = new StateChase(enemy, agent, anim, player);
                stage = EVENT.EXIT;
            }
            else if(Time.time - startTime > 2 && agent.remainingDistance < 2f)
            {
                nextState = new StateIdle(enemy, agent, anim, player);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            // anim.ResetTrigger("isIdle");
            base.Exit();
        }
    }
}