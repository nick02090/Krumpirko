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

        void Start()
        {
            enemyCharacter = GetComponent<EnemyCharacter>();
        }

        void Update()
        {

        }
    }
}
