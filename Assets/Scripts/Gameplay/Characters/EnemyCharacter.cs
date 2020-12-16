using System.Collections.Generic;
using UnityEngine;
using Gameplay.Ailments;
using Gameplay.Actions;
using Gameplay.SceneObjects;
using Core;

namespace Gameplay.Characters
{
    [RequireComponent(typeof(PommesEater))]
    public class EnemyCharacter : MonoBehaviour, ICharacter
    {
        public AilmentImmunity ailmentImmunity;

        /// <summary>
        /// Current status of the player that defines it's various properties
        /// </summary>
        private List<Ailment> ailments;
        /// <summary>
        /// Holds the information about eating.
        /// </summary>
        private PommesEater pommesEater;

        private void Start()
        {
            // Initialize member variables
            ailments = new List<Ailment>();
            pommesEater = GetComponent<PommesEater>();
            // Subscribe to eater delegates
            pommesEater.onStateChange += OnEaterStateChange;
            pommesEater.onDeath += OnEaterDeath;
            pommesEater.onHotSauce += OnHotSauce;
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
            // Collision with the player
            if (collision.gameObject.CompareTag("Player"))
            {
                // TODO
                // Steal pommes from player and add them to your capacity
            }
            // Collision with pickable object
            if (collision.collider.TryGetComponent(out SceneObject sceneObject))
            {
                if (!sceneObject.IsPickable())
                {
                    sceneObject.Interact(this);
                }
            }
        }

        #region Pommes eater delegates
        /// <summary>
        /// Called when eater hasn't eaten anything for a while.
        /// </summary>
        private void OnEaterStateChange()
        {
            //Debug.Log($"Enemy eater has changed state to {pommesEater.State.DescriptionAttr()}.");
        }

        /// <summary>
        /// Called when eaters state has change (Eating -> Starving and vice versa)
        /// </summary>
        private void OnEaterDeath()
        {
            //Destroy(gameObject);
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
            throw new System.NotImplementedException();
        }

        public void SetMovementSpeed(float movementSpeed)
        {
            throw new System.NotImplementedException();
        }

        public Transform GetTransform()
        {
            throw new System.NotImplementedException();
        }

        public void SpillHotSauce()
        {
            throw new System.NotImplementedException();
        }

        public bool CanBaitWithHotSauce()
        {
            throw new System.NotImplementedException();
        }

        public int BaitWithHotSauce()
        {
            throw new System.NotImplementedException();
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
            ailments.Add(ailment);
        }

        public void DropAllPommes()
        {
            throw new System.NotImplementedException();
        }

        public void AddGum()
        {
            throw new System.NotImplementedException();
        }

        public bool IsImmuneTo(AilmentType ailmentType)
        {
            return ailmentImmunity.GetImmunity(ailmentType);
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
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
