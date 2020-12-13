using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Ailments;
using Gameplay.Actions;
using Core;

namespace Gameplay.Characters
{
    [RequireComponent(typeof(PommesEater))]
    public class PlayerCharacter : MonoBehaviour, ICharacter
    {
        /// <summary>
        /// Determines the speed of the player movement.
        /// </summary>
        public float movementSpeed = 1.0f;

        public GameObject baitPrefab;
        public Transform baitParent;

        public ParticleSystem coughParticle;
        public Transform coughParent;

        public GameObject ketchupPrefab;
        public Transform ketchupParent;

        public ActionCosts actionCosts;

        /// <summary>
        /// Current status of the player that defines it's various properties
        /// </summary>
        private List<Ailment> ailments;
        /// <summary>
        /// Holds the information about eating.
        /// </summary>
        private PommesEater pommesEater;

        // TODO: Export this somewhere outside from the player definition
        private Dictionary<ActionType, IAction> actionMapping;

        void Start()
        {
            // Initialize member variables
            ailments = new List<Ailment>();
            pommesEater = GetComponent<PommesEater>();
            // Subscribe to eater delegates
            pommesEater.onStateChange += OnEaterStateChange;
            pommesEater.onDeath += OnEaterDeath;
            pommesEater.onHotSauce += OnHotSauce;
            // Initialize action mappings
            actionMapping = new Dictionary<ActionType, IAction>()
            {
                { ActionType.Bait, new BaitAction(baitPrefab, baitParent) },
                { ActionType.Cough, new CoughAction(coughParticle, coughParent) },
                { ActionType.Gum, new GumAction() },
                { ActionType.HotSauce, new HotSauceAction() },
                { ActionType.Ketchup, new KetchupAction(ketchupPrefab, ketchupParent) }
            };
        }

        void Update()
        {
            // Update ailments based on their timers
        }

        public bool CanExecuteAction(ActionType actionType)
        {
            if (actionMapping.TryGetValue(actionType, out IAction action))
            {
                int actionCost = actionCosts.GetCost(actionType);
                ActionCostType actionCostType = action.GetCostType();
                return pommesEater.HasCapacityFor(actionCost, actionCostType);
            }
            return false;
        }

        public void ExecuteAction(ActionType actionType)
        {
            if (actionMapping.TryGetValue(actionType, out IAction action))
            {
                // Update eater current pommes capacity
                int actionCost = actionCosts.GetCost(actionType);
                ActionCostType actionCostType = action.GetCostType();
                pommesEater.ExecuteAction(actionCost, actionCostType);
                // Execute the action
                action.Execute(this);
            }
            else
            {
                Debug.LogError("Can't find action cost value!");
            }
        }

        /// <summary>
        /// Called when eater hasn't eaten anything for a while.
        /// </summary>
        private void OnEaterDeath()
        {
            // TODO: Update player properties so they math the current eater state.
            Debug.Log($"Player eater has died.");
        }

        /// <summary>
        /// Called when eaters state has change (Eating -> Starving and vice versa)
        /// </summary>
        private void OnEaterStateChange()
        {
            // TODO: Update player properties so they match the current eater state.
            Debug.Log($"Player eater has changed state to {pommesEater.State.DescriptionAttr()}.");
        }

        private void OnHotSauce()
        {
            // TODO: Update player properties so they match the current eater state.
            Debug.Log($"Player eater has ate a pommes that has hot sauce on it.");
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
            return movementSpeed;
        }

        public void SetMovementSpeed(float movementSpeed)
        {
            throw new System.NotImplementedException();
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void SpillHotSauce()
        {
            pommesEater.SpillHotSauce();
        }

        public bool CanBaitWithHotSauce()
        {
            return pommesEater.HotSaucePommesCapacity > 0;
        }

        public int BaitWithHotSauce()
        {
            // Calculate how many pommes you have with hot sauce on them
            int numberOfHotSaucePommes = Mathf.Clamp(pommesEater.HotSaucePommesCapacity, 0, 10);
            // Remove those from the count
            pommesEater.HotSaucePommesCapacity -= numberOfHotSaucePommes;
            // TODO: Check with the others from the team if you should keep this or not
            // Since you lost some from the capacity but you actually used those with hot sauce as a bait
            // recover the lost pommes in the capacity
            pommesEater.PommesCapacity += numberOfHotSaucePommes;
            return numberOfHotSaucePommes;
        }

        public void DisableAction(ActionType actionType)
        {
            throw new System.NotImplementedException();
        }

        public void EnableAction(ActionType actionType)
        {
            throw new System.NotImplementedException();
        }

        public void InflictWith(Ailment ailment)
        {
            throw new System.NotImplementedException();
        }

        public void DropAllPommes()
        {
            throw new System.NotImplementedException();
        }
    }
}
