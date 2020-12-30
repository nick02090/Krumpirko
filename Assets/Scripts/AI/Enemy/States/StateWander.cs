using UnityEngine;
using UnityEngine.AI;
using Gameplay.Characters;

namespace AI.Enemy.States
{
    public class StateWander : EnemyState
    {
        private float startTime;

        public StateWander(GameObject _enemy, NavMeshAgent _agent, Animator _anim, GameObject _player, EnemyCharacter _enemyCharacter)
                    : base(_enemy, _agent, _anim, _player, _enemyCharacter)
        {
            name = STATE.WANDER;
        }

        public override void Enter()
        {
            startTime = Time.time;
            agent.isStopped = false;

            SetRandomDestinationFromRing(aiParameters.WanderMinRingDistance, aiParameters.WanderMaxRingDistance);

            base.Enter();
        }

        public override void Update()
        {       
            if (AreThereBaits()) 
            {
                if (GetClosestBaitInRange() != null) 
                {
                    nextState = new StateBait(enemy, agent, anim, player, enemyCharacter);
                    stage = EVENT.EXIT;
                    return;
                }
            }
            
            if (IsStarving()) {
                    nextState = new StateStarving(enemy, agent, anim, player, enemyCharacter);
                    stage = EVENT.EXIT;
            }
            else if (CanSeePlayer())
            {
                nextState = new StateChase(enemy, agent, anim, player, enemyCharacter);
                stage = EVENT.EXIT;
            }
            else 
            {
                // if (canHearPlayer())
                // {
                //     turnTowardsPlayer(aiParameters.NormalRotationSpeed);
                // }

                if (Time.time - startTime >= aiParameters.MinWanderingTime)
                {
                    if (RandomChance(aiParameters.FromWanderToIdleChance))
                    {
                        nextState = new StateIdle(enemy, agent, anim, player, enemyCharacter);
                        stage = EVENT.EXIT;
                    }
                    else if (agent.remainingDistance < aiParameters.WanderRemainingDis)
                    {
                        SetRandomDestinationFromRing(aiParameters.WanderMinRingDistance, aiParameters.WanderMaxRingDistance);
                    }
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}