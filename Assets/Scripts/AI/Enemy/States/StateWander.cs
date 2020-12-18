using UnityEngine;
using UnityEngine.AI;
using Gameplay.Characters;

namespace AI.Enemy.States
{
    public class StateWander : EnemyState
    {
        private float startTime;

        public StateWander(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player, EnemyCharacter _enemyCharacter)
                    : base(_enemy, _agent, _anim, _player, _enemyCharacter)
        {
            name = STATE.WANDER;
        }

        public override void Enter()
        {
            startTime = Time.time;
            agent.isStopped = false;

            SetRandomDestination(-aiParameters.wanderingRange, aiParameters.wanderingRange);

            base.Enter();
        }

        public override void Update()
        {
            if(canHearPlayer())
            {
                turnTowardsPlayer(aiParameters.normalRotationSpeed);
            }

            if (CanSeePlayer())
            {
                nextState = new StateChase(enemy, agent, anim, player, enemyCharacter);
                stage = EVENT.EXIT;
            }
            else if (Time.time - startTime < aiParameters.wanderingTime && agent.remainingDistance < aiParameters.wanderingRemainingDis)
            {
                startTime = Time.time;
                SetRandomDestination(-aiParameters.wanderingRange, aiParameters.wanderingRange);
            }
            else if(Time.time - startTime < aiParameters.wanderingTime && RandomChance(aiParameters.fromWanderToIdleChance))
            {
                nextState = new StateIdle(enemy, agent, anim, player, enemyCharacter);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}