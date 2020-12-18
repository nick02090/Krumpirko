using UnityEngine;
using Gameplay.Ailments;
using Gameplay.SceneObjects;
using System.Collections.Generic;
using AI.Enemy;

namespace Gameplay.Characters
{
    [RequireComponent(typeof(PommesEater))]
    [RequireComponent(typeof(AilmentHandler))]
    public class EnemyCharacter : MonoBehaviour, ICharacter
    {
        /// <summary>
        /// Holds the information about ailments.
        /// </summary>
        public AilmentHandler AilmentHandler { get; private set; }
        /// <summary>
        /// Holds the information about eating.
        /// </summary>
        public PommesEater PommesEater { get; private set; }
        /// <summary>
        /// Holds the information necessary for enemy AI.
        /// </summary>
        public EnemyAIParameters aiParameters {get; set;}

        private void Start()
        {
            // Initialize member variables
            AilmentHandler = GetComponent<AilmentHandler>();
            PommesEater = GetComponent<PommesEater>();
            aiParameters = EnemyAIParameters.Instance;
            // Subscribe to eater delegates
            PommesEater.onStateChange += OnEaterStateChange;
            PommesEater.onDeath += OnEaterDeath;
            PommesEater.onHotSauce += OnHotSauce;
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
                // Steal pommes from player and add them to your capacity
                PommesEater playersPommesEater = collision.gameObject.GetComponent<PommesEater>();
                if (playersPommesEater.GumCapacity > 0)
                {
                    playersPommesEater.RemoveGum();
                    InflictWith(new HappyAilment());
                }
                else if (playersPommesEater.PommesCapacity > 0)
                {
                    int remainingPommes = PommesEater.LeftCapacity;
                    int numberOfTakenPommes = Mathf.Clamp(remainingPommes, 0, playersPommesEater.PommesCapacity);
                    int numberOfHotPommes = Mathf.Clamp(playersPommesEater.HotSaucePommesCapacity, 0, remainingPommes);
                    PommesEater.AddPommes(numberOfTakenPommes, numberOfHotPommes);
                    playersPommesEater.RemovePommes(numberOfTakenPommes, 0);
                }
            }
            // Collision with pickable object
            if (collision.gameObject.TryGetComponent(out SceneObject sceneObject))
            {
                if (sceneObject.IsPickable())
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
                AilmentHandler.AddAilment(new BurnAilment());
            }
        }
        #endregion

        #region ICharacter
        public float GetMovementSpeed()
        {
            // throw new System.NotImplementedException();
            return 2.0f;
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

        public void InflictWith(Ailment ailment)
        {
            AilmentHandler.AddAilment(ailment);
        }

        public void AddGum()
        {
            throw new System.NotImplementedException();
        }

        public bool IsImmuneTo(AilmentType ailmentType)
        {
            return AilmentHandler.IsImmune(ailmentType);
        }

        public int GetLeftPommesCapacity()
        {
            return PommesEater.LeftCapacity;
        }

        public void AddPommes(int totalNumberOfPommes, int numberOfHotPommes)
        {
            PommesEater.AddPommes(totalNumberOfPommes, numberOfHotPommes);
        }

        public bool HasTag(string tag)
        {
            return CompareTag(tag);
        }

        public float GetRotationSpeed()
        {
            throw new System.NotImplementedException();
        }

        public void SetMaxPommesCapacity(int maxCapacity)
        {
            PommesEater.MaxPommesCapacity = maxCapacity;
        }

        public List<Ailment> GetAilments()
        {
            return AilmentHandler.Ailments;
        }

        public int GetMaxPommesCapacity()
        {
            return PommesEater.MaxPommesCapacity;
        }

        public float GetEatingRate()
        {
            return PommesEater.EatingRate;
        }

        public void SetEatingRate(float eatingRate)
        {
            PommesEater.EatingRate = eatingRate;
        }

        public float GetDeathClock()
        {
            return PommesEater.DeathClock;
        }

        public void SetDeathClock(float deathClock)
        {
            PommesEater.DeathClock = deathClock;
        }

        public void AddBait(GameObject bait)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveBait(GameObject bait)
        {
            throw new System.NotImplementedException();
        }

        List<GameObject> ICharacter.GetBaits()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
