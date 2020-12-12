using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class PommesEater : MonoBehaviour
    {
        public enum EaterState
        {
            Eating,
            Starving
        };

        /// <summary>
        /// Pommes per seconds.
        /// </summary>
        [SerializeField] float eatingRate;
        /// <summary>
        /// Seconds to live without any food.
        /// </summary>
        [SerializeField] float deathClock;
        /// <summary>
        /// Maximum capacity of the pommes that eater can hold.
        /// </summary>
        [SerializeField] int maxPommesCapacity = 1;

        /// <summary>
        /// Seconds since the eater last ate.
        /// </summary>
        public float TimeSinceLastAction { get; private set; }
        /// <summary>
        /// Current capacity of the pommes.
        /// </summary>
        public int PommesCapacity { get; private set; }
        /// <summary>
        /// Current state.
        /// </summary>
        public EaterState State { get; set; }

        /// <summary>
        /// Determines whether the eater can hold any more food.
        /// </summary>
        public bool HasAnyCapacity => PommesCapacity < maxPommesCapacity;
        /// <summary>
        /// Number of the left capacity.
        /// </summary>
        public int LeftCapacity => maxPommesCapacity - PommesCapacity;
        /// <summary>
        /// Determines whether the eater can hold extra given amount of food.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool CanHold(int amount) => maxPommesCapacity + amount < maxPommesCapacity;

        void Start()
        {
            // Initialize member variables
            State = EaterState.Starving;
            PommesCapacity = 0;
            TimeSinceLastAction = 0;
        }

        void Update()
        {
            // Update PommesCapacity if the eater has any food (based on its eating rate)

            // Update State if necessary

            // Update TimeSinceLastAction via Time (or something similar) based on the State change
        }
    }
}
