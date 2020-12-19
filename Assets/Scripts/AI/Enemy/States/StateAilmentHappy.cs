using UnityEngine;
using UnityEngine.AI;
using Gameplay.Characters;
using Gameplay.Ailments;

namespace AI.Enemy.States
{
    public class StateAilmentHappy : EnemyState
    {

        private Ailment ailment;

        public StateAilmentHappy(GameObject _enemy, NavMeshAgent _agent, Animator _anim, GameObject _player, EnemyCharacter _enemyCharacter, Ailment ailment)
                    : base(_enemy, _agent, _anim, _player, _enemyCharacter)
        {
            this.ailment = ailment;
            name = STATE.HAPPY;
        }

        public override void Enter()
        {
            Debug.Log("I'm happy");
            agent.isStopped = false;
            base.Enter();
        }

        public override void Update()
        {
        }

        public override void Exit()
        {
            base.Exit();
        }
        
    }
}