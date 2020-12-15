using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Ailments;
using Gameplay.Actions;

namespace Gameplay.Characters
{
    [RequireComponent(typeof(PommesEater))]
    public class EnemyCharacter : MonoBehaviour, ICharacter
    {
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
            throw new System.NotImplementedException();
        }

        public void DropAllPommes()
        {
            throw new System.NotImplementedException();
        }

        public void AddGum()
        {
            throw new System.NotImplementedException();
        }
    }
}
