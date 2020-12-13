﻿using Gameplay.Actions;
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

        public delegate void Death();
        public Death onDeath;

        public delegate void HotSauce();
        public HotSauce onHotSauce;

        /// <summary>
        /// Seconds per pommes.
        /// </summary>
        [SerializeField] public float eatingRate;
        /// <summary>
        /// Seconds to live without any food.
        /// </summary>
        [SerializeField] public float deathClock;
        /// <summary>
        /// Maximum capacity of the pommes that eater can hold.
        /// </summary>
        [SerializeField] public int maxPommesCapacity;
        /// <summary>
        /// Flag that determines whether the eater will eat hot sauce pommes.
        /// </summary>
        [SerializeField] public bool eatsHotSauce = false;
        /// <summary>
        /// Flag that determines whether the eater won't be affected with ailment after eating hot sauce pommes.
        /// </summary>
        [SerializeField] public bool immuneToHotSauce = false;

        /// <summary>
        /// Current capacity of the pommes.
        /// </summary>
        public int PommesCapacity; // TODO: Set to private once you finished debugging
        /// <summary>
        /// Number of eaten pommes.
        /// </summary>
        public int PommesEaten; // TODO: Set to private once you finished debugging
        /// <summary>
        /// Current state.
        /// </summary>
        public EaterState State; // TODO: Set to private once you finished debugging
        /// <summary>
        /// Current capacity of the pommes that have hot sauce over itself.
        /// </summary>
        public int HotSaucePommesCapacity; // TODO: Set to private once you finished debugging

        private float timer = 0.0f;

        public void Start()
        {
            // Initialize member variables
            ChangeState(EaterState.Starving);
            PommesCapacity = 100;
        }

        public void Update()
        {
            // Calculate elapsed time from last frame (seconds)
            timer += Time.deltaTime;

            // Update PommesCapacity if the eater has any food (based on its eating rate)
            if (State == EaterState.Eating)
            {
                // If it's time to eat -> eat another pommes
                if (timer >= eatingRate)
                {
                    // Check for hot sauce pommes
                    if (eatsHotSauce && HotSaucePommesCapacity > 0)
                    {
                        // Eat one hot sauce pommes
                        HotSaucePommesCapacity--;
                        if (!immuneToHotSauce)
                        {
                            // Invoke the subscribers on eater hot sauce eating if he's not immune to hot sauce
                            onHotSauce?.Invoke();
                        }
                    }
                    else
                    {
                        // Eat one pommes
                        PommesCapacity--;
                    }
                    // Reset timer
                    timer = 0.0f;
                    // Update pommes eaten number
                    PommesEaten++;
                }
            }
            // Check if the eater is still alive
            else
            {
                if (timer >= deathClock)
                {
                    // Invoke the subscribers on eater death
                    onDeath?.Invoke();
                }
            }

            // Update State
            if (PommesCapacity <= 0 || (eatsHotSauce && HotSaucePommesCapacity <= 0))
            {
                ChangeState(EaterState.Starving);
            }
            else
            {
                ChangeState(EaterState.Eating);
            }
        }

        private void ChangeState(EaterState state)
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
                // Invoke the subscribers on state change
                onStateChange?.Invoke();
            }
        }

        public void SpillHotSauce()
        {
            HotSaucePommesCapacity += 30;
        }

        public void ExecuteAction(int actionCost, ActionCostType actionCostType)
        {
            switch(actionCostType)
            {
                case ActionCostType.PommesCapacity:
                    PommesCapacity -= actionCost;
                    break;
                case ActionCostType.PommesEaten:
                    PommesEaten -= actionCost;
                    break;
                default:
                    break;
            }
        }

        public bool HasCapacityFor(int actionCost, ActionCostType actionCostType)
        {
            switch(actionCostType)
            {
                case ActionCostType.PommesCapacity:
                    return actionCost <= PommesCapacity;
                case ActionCostType.PommesEaten:
                    return actionCost <= PommesEaten;
                default:
                    return false;
            }
        }

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
    }
}