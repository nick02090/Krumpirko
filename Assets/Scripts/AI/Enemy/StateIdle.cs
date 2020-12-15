using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Enemy
{
    public class StateIdle : EnemyState
    {
        private float startTime;

        public StateIdle(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player)
                    : base(_enemy, _agent, _anim, _player)
        {
            name = STATE.IDLE;
            agent.isStopped = true;

            startTime = Time.time;
        }

        public override void Enter()
        {
            Debug.Log("I'm in Idle");

            // anim.SetTrigger("isIdle"); 
            base.Enter();
        }

        public override void Update()
        {
            if (CanSensePlayer())
            {
                nextState = new StateChase(enemy, agent, anim, player);
                stage = EVENT.EXIT;
            }
            else if(Time.time - startTime > 1.5f)
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
}