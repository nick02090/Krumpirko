using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Gameplay
{
    public class PommesEater : MonoBehaviour
    {
        public enum EaterState
        {
            [Description("Eating")]
            Eating,
            [Description("Starving")]
            Starving
        };

        public delegate void StateChange();
        public StateChange onStateChange;

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
        public float TimeSinceLastAction; // TODO: Set to private once you finished debugging
        /// <summary>
        /// Current capacity of the pommes.
        /// </summary>
        public int PommesCapacity; // TODO: Set to private once you finished debugging
        /// <summary>
        /// Current state.
        /// </summary>
        public EaterState State; // TODO: Set to private once you finished debugging

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
            ChangeState(EaterState.Starving);
            PommesCapacity = 200;
            TimeSinceLastAction = 0;
        }

        void Update()
        {
            // Update PommesCapacity if the eater has any food (based on its eating rate)

            // Update State
            if (PommesCapacity <= 0)
            {
                ChangeState(EaterState.Starving);
            }
            else
            {
                ChangeState(EaterState.Eating);
            }

            // Update TimeSinceLastAction via Time (or something similar) based on the State change
        }

        void ChangeState(EaterState state)
        {
            // State stays unchanged
            if (state == State)
            {
                return;
            }
            // State has changed
            else
            {
                State = state;
                // Invoke the delegates that are subscribed
                onStateChange?.Invoke();
            }
        }
    }
}
