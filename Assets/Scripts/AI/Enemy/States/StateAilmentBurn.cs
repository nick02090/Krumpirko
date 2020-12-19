using UnityEngine;
using UnityEngine.AI;
using Gameplay.Characters;
using Gameplay.Ailments;

namespace AI.Enemy.States
{
    public class StateAilmentBurn : EnemyState
    {

        private Ailment ailment;
        private Vector3 position1;
        private Vector3 position2;

        public StateAilmentBurn(GameObject _enemy, NavMeshAgent _agent, Animator _anim, GameObject _player, EnemyCharacter _enemyCharacter, Ailment ailment)
                    : base(_enemy, _agent, _anim, _player, _enemyCharacter)
        {
            this.ailment = ailment;
            name = STATE.BURN;
        }

        public override void Enter()
        {
            Debug.Log("I'm burnging");

            agent.isStopped = false;
            Vector3 offset1 = new Vector3(RandomCoordinate(2.0f, 3.0f), 0f, RandomCoordinate(2.0f, 3.0f));
            position1 = enemy.transform.position + offset1;

            Vector3 offset2 = new Vector3(RandomCoordinate(3.0f, 4.0f), 0f, RandomCoordinate(3.0f, 4.0f));
            position2 = position1 + offset2;

            agent.SetDestination(position1);

            base.Enter();
        }

        public override void Update()
        {
            if (ailment.HasFinished)
            {
                nextState = new StateIdle(enemy, agent, anim, player, enemyCharacter);
                stage = EVENT.EXIT;
            }
            else 
            {
                agent.speed = enemyCharacter.GetMovementSpeed() * aiParameters.BurnSpeedMultiplayer;
                if (agent.remainingDistance < 0.5f)
                {
                    agent.SetDestination(position2);
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
        }

        private float RandomCoordinate(float low, float high) 
        {
            float num = Random.Range(low, high);
            if (RandomChance(50))
            {
                num *= -1;
            }
            return num;
        }
        
    }
}