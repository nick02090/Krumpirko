using UnityEngine;
using UnityEngine.AI;
using Gameplay.Characters;

namespace AI.Enemy.States
{
    public class StateAilmentFear : EnemyState
    {

        public StateAilmentFear(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player, EnemyCharacter _enemyCharacter)
                    : base(_enemy, _agent, _anim, _player, _enemyCharacter)
        {
            name = STATE.FEAR;
        }

        public override void Enter()
        {
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