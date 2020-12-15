using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Enemy
{
    public class StateChase : EnemyState
    {
        public StateChase(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player)
                    : base(_enemy, _agent, _anim, _player)
        {
            name = STATE.CHASE;
            agent.isStopped = false;

            visAngle = 20f;
            agent.speed = 5;
        }

        public override void Enter()
        {
            Debug.Log("I'm in Chase");

            // anim.SetTrigger("isRunning");
            agent.SetDestination(player.position);

            base.Enter();
        }

        public override void Update()
        {
            if (CanStealFries())
            {
                nextState = new StateEating(enemy, agent, anim, player);
                stage = EVENT.EXIT;
            }
            else if (!CanSensePlayer() && Random.Range(0, 100) < 10)
            {
                nextState = new StateWander(enemy, agent, anim, player);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            // anim.ResetTrigger("isRunning");
            visAngle = 45f;
            base.Exit();
        }
    }
}