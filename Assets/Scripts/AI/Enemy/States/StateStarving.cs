using UnityEngine;
using UnityEngine.AI;
using Gameplay.Characters;

namespace AI.Enemy.States
{
    public class StateStarving : EnemyState
    {

        public StateStarving(GameObject _enemy, NavMeshAgent _agent, Animator _anim, GameObject _player, EnemyCharacter _enemyCharacter)
                    : base(_enemy, _agent, _anim, _player, _enemyCharacter)
        {
            name = STATE.STARVING;
        }

        public override void Enter()
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);

            base.Enter();
        }

        public override void Update()
        {
            // TODO dio gameplaya da samo na igrača ide dok gladuje
            // if (AreThereBaits()) 
            // {
            //     if (GetClosestBaitInRange() != null) 
            //     {
            //         nextState = new StateBait(enemy, agent, anim, player, enemyCharacter);
            //         stage = EVENT.EXIT;
            //         return;
            //     }
            // }

            if (enemyCharacter.IsAilmentActive())
            {
                nextState = GetAilmentState();
                stage = EVENT.EXIT;
            }
            // else if (!IsStarving())
            // {
            //     nextState = new StateIdle(enemy, agent, anim, player, enemyCharacter);
            //     stage = EVENT.EXIT;
            // }
            else if (enemyCharacter.GetLeftPommesCapacity() == 0)
            {
                nextState = new StateEating(enemy, agent, anim, player, enemyCharacter);
                stage = EVENT.EXIT;
            }
            else 
            {
                agent.speed = enemyCharacter.GetMovementSpeed() * aiParameters.StarvingSpeedMultiplayer;
                turnTowardsPlayer(aiParameters.FastRotationSpeed);
                agent.SetDestination(player.transform.position);
            }

        }

        public override void Exit()
        {
            base.Exit();
        }
        
    }
}