using UnityEngine;
using UnityEngine.AI;
using Gameplay.Characters;

namespace AI.Enemy.States
{
    public class StateChase : EnemyState
    {
        public StateChase(GameObject _enemy, NavMeshAgent _agent, Animator _anim, GameObject _player, EnemyCharacter _enemyCharacter)
                    : base(_enemy, _agent, _anim, _player, _enemyCharacter)
        {
            name = STATE.CHASE;
        }

        public override void Enter()
        {
            agent.isStopped = false;

            visAngle = aiParameters.ChaseVisAngle;
            agent.SetDestination(player.transform.position);

            base.Enter();
        }

        public override void Update()
        {
            if (enemyCharacter.IsAilmentActive())
            {
                nextState = GetAilmentState();
                stage = EVENT.EXIT;
                return;
            }

            if (AreThereBaits()) 
            {
                if (GetClosestBaitInRange() != null) 
                {
                    nextState = new StateBait(enemy, agent, anim, player, enemyCharacter);
                    stage = EVENT.EXIT;
                    return;
                }
            }

            if (IsStarving()) 
            {
                    nextState = new StateStarving(enemy, agent, anim, player, enemyCharacter);
                    stage = EVENT.EXIT;
            }
            else if (enemyCharacter.GetLeftPommesCapacity() == 0)
            {
                nextState = new StateEating(enemy, agent, anim, player, enemyCharacter);
                stage = EVENT.EXIT;
            }
            else if (!CanSeePlayer() && RandomChance(aiParameters.FromChaseToIdleChance))
            {
                nextState = new StateIdle(enemy, agent, anim, player, enemyCharacter);
                stage = EVENT.EXIT;
            } 
            else
            {
                agent.speed = enemyCharacter.GetMovementSpeed() * aiParameters.ChaseSpeedMultiplayer;
                turnTowardsPlayer(aiParameters.FastRotationSpeed);
                agent.SetDestination(player.transform.position);
            }
        }

        public override void Exit()
        {
            visAngle = aiParameters.NormalVisAngle;
            base.Exit();
        }
    }
}