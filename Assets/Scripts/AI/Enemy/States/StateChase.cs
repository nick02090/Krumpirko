using UnityEngine;
using UnityEngine.AI;
using Gameplay.Characters;

namespace AI.Enemy.States
{
    public class StateChase : EnemyState
    {
        public StateChase(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player, EnemyCharacter _enemyCharacter)
                    : base(_enemy, _agent, _anim, _player, _enemyCharacter)
        {
            name = STATE.CHASE;
        }

        public override void Enter()
        {
            agent.isStopped = false;

            visAngle = aiParameters.chaseVisAngle;
            // agent.speed = aiParameters.chaseSpeed;
            agent.SetDestination(player.position);

            base.Enter();
        }

        public override void Update()
        {
            agent.speed = enemyCharacter.GetMovementSpeed() * aiParameters.chaseSpeedMultiplayer;

            turnTowardsPlayer(aiParameters.chaseRotationSpeed);

            if (CanStealFries())
            {
                nextState = new StateEating(enemy, agent, anim, player, enemyCharacter);
                stage = EVENT.EXIT;
            }
            else if (!CanSeePlayer() && RandomChance(aiParameters.fromChaseToWanderChance))
            {
                nextState = new StateWander(enemy, agent, anim, player, enemyCharacter);
                stage = EVENT.EXIT;
            } 
            else {
                agent.SetDestination(player.position);
            }
        }

        public override void Exit()
        {
            visAngle = aiParameters.normalVisAngle;
            base.Exit();
        }
    }
}