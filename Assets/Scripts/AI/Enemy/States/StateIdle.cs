using UnityEngine;
using UnityEngine.AI;
using Gameplay.Characters;

namespace AI.Enemy.States
{
    public class StateIdle : EnemyState
    {
        private float startTime;

        public StateIdle(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player, EnemyCharacter _enemyCharacter)
                    : base(_enemy, _agent, _anim, _player, _enemyCharacter)
        {
            name = STATE.IDLE;
        }

        public override void Enter()
        {
            startTime = Time.time;
            agent.isStopped = true;

            base.Enter();
        }

        public override void Update()
        {
            if(canHearPlayer())
            {
                turnTowardsPlayer(aiParameters.normalRotationSpeed);
            }

            if(CanSeePlayer())
            {
                nextState = new StateChase(enemy, agent, anim, player, enemyCharacter);
                stage = EVENT.EXIT;
            }
            else if(Time.time - startTime > aiParameters.idlingTime)
            {
                nextState = new StateWander(enemy, agent, anim, player, enemyCharacter);
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