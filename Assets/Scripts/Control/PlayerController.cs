using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Characters;

namespace Control
{
    [RequireComponent(typeof(PlayerCharacter))]
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// Holds the information about the player.
        /// </summary>
        private PlayerCharacter playerCharacter;

        /// <summary>
        /// Current direction in which the player should move.
        /// </summary>
        public Vector2 MovementDirection { get; set; }

        void Start()
        {
            // Initialize member variables
            MovementDirection = new Vector2(0.0f, 0.0f);
        }

        void Update()
        {
            // Check for input and play an action if available

            // Move player (based on input)
        }
    }
}
