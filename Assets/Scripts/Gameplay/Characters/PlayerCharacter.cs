using System.Collections.Generic;
using UnityEngine;
using Gameplay.Ailments;
using Gameplay.Actions;
using Gameplay.SceneObjects;

namespace Gameplay.Characters
{
    [RequireComponent(typeof(PommesEater))]
    public class PlayerCharacter : MonoBehaviour, ICharacter
    {
        /// <summary>
        /// Determines the speed of the player movement.
        /// </summary>
        public float MovementSpeed = 1.0f;
        /// <summary>
        /// Determines the speed of the player rotation.
        /// </summary>
        public float RotationSpeed = 180.0f;

        public ActionCosts ActionCosts;
        public ActionMappings ActionMappings;
        public AilmentImmunity AilmentImmunity;

        /// <summary>
        /// Current status of the player that defines it's various properties
        /// </summary>
        private List<Ailment> ailments;
        /// <summary>
        /// Holds the information about eating.
        /// </summary>
        private PommesEater pommesEater;

        public SceneObject CurrentCollider { get; private set; }

        void Start()
        {
            // Initialize member variables
            ailments = new List<Ailment>();
            pommesEater = GetComponent<PommesEater>();
            // Subscribe to eater delegates
            pommesEater.onStateChange += OnEaterStateChange;
            pommesEater.onDeath += OnEaterDeath;
            pommesEater.onHotSauce += OnHotSauce;
            CurrentCollider = null;
        }

        public bool CanExecuteAction(ActionType actionType)
        {
            if (ActionMappings.TryGetAction(actionType, out IAction action))
            {
                int actionCost = ActionCosts.GetCost(actionType);
                ActionCostType actionCostType = action.GetCostType();
                return pommesEater.HasCapacityFor(actionCost, actionCostType);
            }
            return false;
        }

        public void ExecuteAction(ActionType actionType)
        {
            if (ActionMappings.TryGetAction(actionType, out IAction action))
            {
                // Update eater current pommes capacity
                int actionCost = ActionCosts.GetCost(actionType);
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

        public bool TryGetCollider(out SceneObject sceneObject)
        {
            sceneObject = CurrentCollider;
            if (sceneObject != null)
            {
                return true;
            }
            return false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out SceneObject sceneObject))
            {
                if (!sceneObject.IsPickable())
                {
                    sceneObject.Interact(this);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out SceneObject sceneObject))
            {
                if (sceneObject.IsPickable())
                {
                    CurrentCollider = sceneObject;
                    // Highlight the current collider
                    CurrentCollider.ShowHighlight();
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (CurrentCollider != null)
            {
                // Remove the highlight from the collider
                CurrentCollider.HideHighlight();
                CurrentCollider = null;
            }
        }

        #region Pommes eater delegates
        /// <summary>
        /// Called when eater hasn't eaten anything for a while.
        /// </summary>
        private void OnEaterDeath()
        {
            // TODO: Update player properties so they math the current eater state.
            //Debug.Log($"Player eater has died.");
        }

        /// <summary>
        /// Called when eaters state has change (Eating -> Starving and vice versa)
        /// </summary>
        private void OnEaterStateChange()
        {
            // TODO: Update player properties so they match the current eater state.
            //Debug.Log($"Player eater has changed state to {pommesEater.State.DescriptionAttr()}.");
        }

        /// <summary>
        /// Called when eater eats the pommes with hot sauce on it.
        /// </summary>
        private void OnHotSauce()
        {
            if (!IsImmuneTo(AilmentType.Burn))
            {
                ailments.Add(new BurnAilment());
            }
        }
        #endregion

        #region ICharacter
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
            return MovementSpeed;
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
            int numberOfHotSaucePommes = Mathf.Clamp(pommesEater.HotSaucePommesCapacity, 0, PommesBatch.SIZE);
            // Since you lost some from the capacity but you actually used those with hot sauce as a bait
            // recover the lost pommes in the capacity
            pommesEater.AddPommes(numberOfHotSaucePommes, 0);
            // Remove those from the count
            pommesEater.RemovePommes(numberOfHotSaucePommes, numberOfHotSaucePommes);
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

        public void AddGum()
        {
            pommesEater.AddGum();
        }

        public bool IsImmuneTo(AilmentType ailmentType)
        {
            return AilmentImmunity.GetImmunity(ailmentType);
        }

        public int GetLeftPommesCapacity()
        {
            return pommesEater.LeftCapacity;
        }

        public void AddPommes(int totalNumberOfPommes, int numberOfHotPommes)
        {
            pommesEater.AddPommes(totalNumberOfPommes, numberOfHotPommes);
        }

        public bool HasTag(string tag)
        {
            return CompareTag(tag);
        }

        public float GetRotationSpeed()
        {
            return RotationSpeed;
        }
        #endregion
    }
}
