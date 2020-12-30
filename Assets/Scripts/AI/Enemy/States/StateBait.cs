using UnityEngine;
using UnityEngine.AI;
using Gameplay.Characters;

namespace AI.Enemy.States
{
    public class StateBait : EnemyState
    {

        private GameObject bait;

        public StateBait(GameObject _enemy, NavMeshAgent _agent, Animator _anim, GameObject _player, EnemyCharacter _enemyCharacter)
                    : base(_enemy, _agent, _anim, _player, _enemyCharacter)
        {
            name = STATE.BAIT;
        }

        public override void Enter()
        {
            agent.isStopped = false;
            bait = GetClosestBaitInRange();

            base.Enter();
        }

        public override void Update()
        {
            if (bait != null) 
            {
                agent.SetDestination(bait.transform.position);
                agent.speed = enemyCharacter.GetMovementSpeed() * aiParameters.BaitSpeedMultiplayer;
                turnXTowardsY(enemy, bait, aiParameters.FastRotationSpeed);

                if (enemyCharacter.GetLeftPommesCapacity() == 0)
                {
                    nextState = new StateEating(enemy, agent, anim, player, enemyCharacter);
                    stage = EVENT.EXIT;
                }
                // else if (agent.remainingDistance < 0.5f)
                // {
                //     nextState = new StateIdle(enemy, agent, anim, player, enemyCharacter);
                //     stage = EVENT.EXIT;
                // }
            }
            else 
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