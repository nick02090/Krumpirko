using UnityEngine;
using UnityEngine.AI;
using Gameplay.Characters;

namespace AI.Enemy.States
{
    public class StateEating : EnemyState
    {

        private float startTime;
        private float eatingTime;

        public StateEating(GameObject _enemy, NavMeshAgent _agent, Animator _anim, GameObject _player, EnemyCharacter _enemyCharacter)
                    : base(_enemy, _agent, _anim, _player, _enemyCharacter)
        {
            name = STATE.EATING;
        }

        public override void Enter()
        {
            startTime = Time.time;
            int pommesNum = enemyCharacter.GetMaxPommesCapacity() - enemyCharacter.GetLeftPommesCapacity();
            eatingTime =  pommesNum * enemyCharacter.GetEatingRate() + aiParameters.MinEatingTime;

            agent.isStopped = false;
            
            SetRandomDestinationFromRing(aiParameters.EatingMinRingDistance, aiParameters.EatingMaxRingDistance);

            base.Enter();
        }

        public override void Update()
        {
            // if (enemyCharacter.IsAilmentActive())
            // {
            //     nextState = GetAilmentState();
            //     stage = EVENT.EXIT;
            // }
            // else
            // {            
                agent.speed = enemyCharacter.GetMovementSpeed() * aiParameters.EatingSpeedMultiplayer;

                if (Time.time - startTime > eatingTime)
                {
                    nextState = new StateIdle(enemy, agent, anim, player, enemyCharacter);
                    stage = EVENT.EXIT;
                }
            // }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}