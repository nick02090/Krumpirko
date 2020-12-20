using UnityEngine;
using UnityEngine.AI;
using Gameplay.Characters;
using Gameplay.Ailments;

namespace AI.Enemy.States
{
    public class StateAilmentFear : EnemyState
    {

        private Ailment ailment;
        private float elapsedTime;

        public StateAilmentFear(GameObject _enemy, NavMeshAgent _agent, Animator _anim, GameObject _player, EnemyCharacter _enemyCharacter, Ailment ailment)
                    : base(_enemy, _agent, _anim, _player, _enemyCharacter)
        {
            this.ailment = ailment;
            name = STATE.FEAR;
        }

        public override void Enter()
        {
            agent.isStopped = false;
            SetRandomDestinationFromRing(aiParameters.FearMinDist, aiParameters.FearMaxDist);

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
                elapsedTime += Time.deltaTime;
                agent.speed = enemyCharacter.GetMovementSpeed() * aiParameters.FearSpeedMultiplayer;

                float dist = Vector3.Distance(player.transform.position, enemy.transform.position);
                if (elapsedTime > aiParameters.FearMinTime && dist < aiParameters.FearMinDist) 
                {
                    elapsedTime = 0;
                    SetRandomDestinationFromRing(aiParameters.FearMinDist, aiParameters.FearMaxDist);
                }
            }
        }

        public override void Exit()
        {
            enemyCharacter.ResetActiveAilment();
            base.Exit();
        }
        
    }
}