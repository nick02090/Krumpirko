using UnityEngine;
using Gameplay.Characters;
using AI;
using AI.Enemy;
using UnityEngine.AI;

namespace Control
{
    [RequireComponent(typeof(EnemyCharacter))]
    public class EnemyController : MonoBehaviour
    {
        /// <summary>
        /// Holds the information about the player.
        /// </summary>
        private EnemyCharacter enemyCharacter;

        Animator anim;
        NavMeshAgent agent;

        FsmAI ai;

        void Start()
        {
            enemyCharacter = GetComponent<EnemyCharacter>();
            
            anim = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            GameObject player = GameObject.FindWithTag("Player");

            EnemyState startingState = new StateEnter(gameObject, agent, anim, player.transform);
            ai = new FsmAI(startingState);
        }

        void Update()
        {
            ai.Update();
        }
    }
}
