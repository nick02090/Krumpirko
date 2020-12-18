using UnityEngine;
using UnityEngine.AI;
using Gameplay.Characters;

namespace AI.Enemy.States
{
    public class StateEating : EnemyState
    {

        private float startTime;

        public StateEating(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player, EnemyCharacter _enemyCharacter)
                    : base(_enemy, _agent, _anim, _player, _enemyCharacter)
        {
            name = STATE.EATING;
        }

        public override void Enter()
        {
            Debug.Log("I'm in Eating");

            startTime = Time.time;
            agent.isStopped = false;
            
            SetRandomDestination(-aiParameters.eatingRange, aiParameters.eatingRange);

            base.Enter();
        }

        public override void Update()
        {
            agent.speed = enemyCharacter.GetMovementSpeed() * aiParameters.eatingSpeedMultiplayer;
            
            if (Time.time - startTime > aiParameters.eatingTime)
            {
                nextState = new StateIdle(enemy, agent, anim, player, enemyCharacter);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {
            // anim.ResetTrigger("isEating");
            base.Exit();
        }
    }
}