using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Enemy
{
    public class StateEating : EnemyState
    {

        private float startTime;

        public StateEating(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player)
                    : base(_enemy, _agent, _anim, _player)
        {
            name = STATE.EATING;
            agent.isStopped = false;

            startTime = Time.time;

            agent.speed = 1f;
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
}