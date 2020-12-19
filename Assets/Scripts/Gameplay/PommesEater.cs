using Core;
using Gameplay.Actions;
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
        public float EatingRate = 1;
        /// <summary>
        /// Seconds to live without any food.
        /// </summary>
        public float DeathClock = 10;
        /// <summary>
        /// Maximum capacity of the pommes that eater can hold.
        /// </summary>
        public int MaxPommesCapacity = 100;
        /// <summary>
        /// Flag that determines whether the eater will eat hot sauce pommes.
        /// </summary>
        public bool EatsHotSauce = false;
        /// <summary>
        /// Flag that determines whether the eater will show information about capacity changes.
        /// </summary>
        public bool ShowCapacityChanges = false;

        /// <summary>
        /// Current capacity of the pommes.
        /// </summary>
        public int PommesCapacity; // TODO: set to private once you finished debugging
        /// <summary>
        /// Number of eaten pommes.
        /// </summary>
        public int PommesEaten; // TODO: set to private once you finished debugging
        /// <summary>
        /// Current capacity of the pommes that have hot sauce over itself.
        /// </summary>
        public int HotSaucePommesCapacity; // TODO: set to private once you finished debugging
        /// <summary>
        /// Current capacity of the chewing gums.
        /// </summary>
        public int GumCapacity; // TODO: set to private once you finished debugging
        /// <summary>
        /// Current state.
        /// </summary>
        public EaterState State; // TODO: set to private once you finished debugging

        private float eatingTimer = 0.0f;

        public MessagePopup messagePopup;
        private readonly Color redColor = Color.red;
        private readonly Color greenColor = Color.green;

        public void Start()
        {
            // Initialize member variables
            ChangeState(EaterState.Starving);
            PommesCapacity = 0;
            PommesEaten = 0;
            HotSaucePommesCapacity = 0;
            GumCapacity = 0;
            // Set message popup show/hide flag
            messagePopup.ShowMessage = ShowCapacityChanges;
        }

        public void Update()
        {
            // Calculate elapsed time from last frame (seconds)
            eatingTimer += Time.deltaTime;

            // Update PommesCapacity if the eater has any food (based on its eating rate)
            if (State == EaterState.Eating)
            {
                // If it's time to eat -> eat another pommes
                if (eatingTimer >= EatingRate)
                {
                    // Check for hot sauce pommes
                    if (EatsHotSauce && HotSaucePommesCapacity > 0)
                    {
                        // Eat one hot sauce pommes
                        HotSaucePommesCapacity--;
                        // Invoke the subscribers on eater hot sauce eating if he's not immune to hot sauce
                        onHotSauce?.Invoke();
                    }
                    else
                    {
                        // Eat one pommes
                        PommesCapacity--;
                    }
                    // Reset timer
                    eatingTimer = 0.0f;
                    // Update pommes eaten number
                    PommesEaten++;
                }
            }

            // Check if the eater is still alive
            else
            {
                if (eatingTimer >= DeathClock)
                {
                    // Invoke the subscribers on eater death
                    onDeath?.Invoke();
                }
            }

            // Update State
            if (PommesCapacity <= 0 || (EatsHotSauce && HotSaucePommesCapacity <= 0 && PommesCapacity <= 0))
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
            ShowChange(actionCost, false);
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

        public void AddPommes(int totalNumberOfPommes, int numberOfHotPommes)
        {
            if (CanHold(totalNumberOfPommes))
            {
                HotSaucePommesCapacity += numberOfHotPommes;
                PommesCapacity += totalNumberOfPommes - numberOfHotPommes;
                ShowChange(totalNumberOfPommes, true);
            }
        }

        public void RemovePommes(int totalNumberOfPommes, int numberOfHotPommes)
        {
            HotSaucePommesCapacity -= numberOfHotPommes;
            PommesCapacity -= totalNumberOfPommes - numberOfHotPommes;
            ShowChange(totalNumberOfPommes, false);
        }

        public void AddGum()
        {
            GumCapacity++;
        }

        public void RemoveGum()
        {
            GumCapacity--;
        }

        private void ShowChange(int value, bool isPositive)
        {
            if (value > 0)
            {
                Color color = isPositive ? greenColor : redColor;
                string sign = isPositive ? "+" : "-";
                string message = $"{sign}{value}";
                messagePopup.ResetTimer(message, color);
            }
        }

        /// <summary>
        /// Number of the left capacity.
        /// </summary>
        public int LeftCapacity => MaxPommesCapacity - (PommesCapacity + HotSaucePommesCapacity);
        /// <summary>
        /// Determines whether the eater can hold extra given amount of food.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool CanHold(int amount) => PommesCapacity + HotSaucePommesCapacity + amount <= MaxPommesCapacity;

        public float GetTimeUntilDeath()
        {
            return DeathClock - eatingTimer;
        }
    }
}
