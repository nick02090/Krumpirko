using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Characters;

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
        UnityEngine.AI.NavMeshAgent agent;

        FsmAI ai;

        void Start()
        {
            enemyCharacter = GetComponent<EnemyCharacter>();
            
            anim = this.GetComponent<Animator>();
            agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
            GameObject player = GameObject.FindWithTag("Player");

            EnemyState startingState = new StateEnter(this.gameObject, agent, anim, player.transform);
            ai = new FsmAI(startingState);
        }

        void Update()
        {
            ai.Update();
        }
    }
}
