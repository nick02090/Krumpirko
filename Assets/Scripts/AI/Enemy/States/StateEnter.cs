using UnityEngine;
using UnityEngine.AI;
using Gameplay.Characters;

namespace AI.Enemy.States
{
    public class StateEnter : EnemyState
    {
        private float startTime;

        public StateEnter(GameObject _enemy, NavMeshAgent _agent, Animator _anim, GameObject _player, EnemyCharacter _enemyCharacter)
                    : base(_enemy, _agent, _anim, _player, _enemyCharacter)
        {
            name = STATE.ENTER;
        }

        public override void Enter()
        {
            startTime = Time.time;
            agent.isStopped = false;

            Vector3 randomVector = new Vector3(
                Random.Range(-aiParameters.EnteringRange, aiParameters.EnteringRange), 
                0f, 
                Random.Range(-aiParameters.EnteringRange, aiParameters.EnteringRange));
            agent.SetDestination(randomVector); 

            hearDist = aiParameters.EnteringHearDist;

            base.Enter();
        }

        public override void Update()
        {
            if (Time.time - startTime > aiParameters.MinEnteringTime)
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

                if (CanSeePlayer())
                {
                    nextState = new StateChase(enemy, agent, anim, player, enemyCharacter);
                    stage = EVENT.EXIT;
                }
                else if (canHearPlayer() || agent.remainingDistance < aiParameters.EnteringRemainingDistance)
                {
                    nextState = new StateIdle(enemy, agent, anim, player, enemyCharacter);
                    stage = EVENT.EXIT;
                }
            }
        }

        public override void Exit()
        {
            hearDist = aiParameters.NormalHearDist;
            base.Exit();
        }
    }
}