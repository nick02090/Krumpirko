using UnityEngine;
using UnityEngine.AI;
using Gameplay.Characters;
using AI;
using AI.Enemy;
using AI.Enemy.States;

namespace Control
{
    [RequireComponent(typeof(EnemyCharacter))]
    public class EnemyController : MonoBehaviour
    {
        private FsmAI ai;

        void Start()
        {           
            Animator anim = GetComponent<Animator>();
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            EnemyCharacter enemyCharacter = GetComponent<EnemyCharacter>();

            GameObject player = GameObject.FindWithTag("Player");

            EnemyState startingState = new StateEnter(gameObject, agent, anim, player, enemyCharacter);

            ai = new FsmAI(startingState);
        }

        void Update()
        {
            ai.Update();
        }
    }
}
