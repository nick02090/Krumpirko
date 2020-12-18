using UnityEngine;
using UnityEngine.AI;
using Gameplay.Characters;

namespace AI.Enemy.States
{
    public class StateEnter : EnemyState
    {
        private float startTime;

        public StateEnter(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player, EnemyCharacter _enemyCharacter)
                    : base(_enemy, _agent, _anim, _player, _enemyCharacter)
        {
            name = STATE.ENTER;
        }

        public override void Enter()
        {
            startTime = Time.time;
            agent.isStopped = false;

            Vector3 randomVector = new Vector3(
                Random.Range(-aiParameters.enteringRange, aiParameters.enteringRange), 
                0f, 
                Random.Range(-aiParameters.enteringRange, aiParameters.enteringRange));
            agent.SetDestination(randomVector); 

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
            else if(Time.time - startTime > aiParameters.enteringTime && agent.remainingDistance < aiParameters.enteringRemainingDis)
            {
                nextState = new StateIdle(enemy, agent, anim, player, enemyCharacter);
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