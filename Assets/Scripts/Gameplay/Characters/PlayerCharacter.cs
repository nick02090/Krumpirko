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
            // Subscribe to eater state change
            pommesEater.onStateChange += OnEaterStateChange;
            // Initialize action mappings
            actionMapping = new Dictionary<ActionType, IAction>()
            {
                { ActionType.Bait, new BaitAction(baitPrefab, baitParent) },
                { ActionType.Cough, new CoughAction() },
                { ActionType.Gum, new GumAction() },
                { ActionType.HotSauce, new HotSauceAction() },
                { ActionType.Ketchup, new KetchupAction() }
            };
        }

        // Update is called once per frame
        void Update()
        {
            // Update ailments based on their timers
        }

        public bool CanExecuteAction(ActionType actionType)
        {
            if (actionMapping.TryGetValue(actionType, out IAction action))
            {
                if (action.GetCost() <= pommesEater.PommesCapacity)
                {
                    return true;
                }
            }
            return false;
        }

        public void ExecuteAction(ActionType actionType)
        {
            if (actionMapping.TryGetValue(actionType, out IAction action))
            {
                // Update eater current pommes capacity
                pommesEater.PommesCapacity -= action.GetCost();
                // Execute the action
                action.Execute(this);
            }
            else
            {
                Debug.LogError("Can't find action cost value!");
            }
        }

        private void OnEaterStateChange()
        {
            // TODO: Update player properties so they match the current eater state.
            Debug.Log($"Eater has changed state to {pommesEater.State.DescriptionAttr()}");
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
    }
}
