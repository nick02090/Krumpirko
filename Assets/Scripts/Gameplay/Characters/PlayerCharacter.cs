using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Ailments;

namespace Gameplay.Characters
{
    [RequireComponent(typeof(PommesEater))]
    public class PlayerCharacter : MonoBehaviour, ICharacter
    {
        /// <summary>
        /// Determines the speed of the player movement.
        /// </summary>
        [Range(1.0f, 10.0f)]
        [SerializeField] float movementSpeed;

        /// <summary>
        /// Current status of the player that defines it's various properties
        /// </summary>
        private List<Ailment> ailments;
        /// <summary>
        /// Holds the information about eating.
        /// </summary>
        private PommesEater pommesEater;

        void Start()
        {
            // Initialize member variables
            ailments = new List<Ailment>();
            pommesEater = GetComponent<PommesEater>();
        }

        // Update is called once per frame
        void Update()
        {
            // Update ailments based on their timers
        }

        public void DisableAllActions()
        {
            throw new System.NotImplementedException();
        }

        public void EnableAllActions()
        {
            throw new System.NotImplementedException();
        }

        public float GetMovementSpeed()
        {
            throw new System.NotImplementedException();
        }

        public void SetMovementSpeed(float movementSpeed)
        {
            throw new System.NotImplementedException();
        }
    }
}
