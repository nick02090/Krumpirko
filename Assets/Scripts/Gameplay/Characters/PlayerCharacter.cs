using UnityEngine;
using Gameplay.Ailments;
using Gameplay.Actions;
using Gameplay.SceneObjects;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Gameplay.Characters
{
    [RequireComponent(typeof(PommesEater))]
    [RequireComponent(typeof(AilmentHandler))]
    [RequireComponent(typeof(ScoreHandler))]
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
        public ScoreSystem ScoreSystem;

        public List<GameObject> Baits { get; private set; }

        /// <summary>
        /// Holds the information about score.
        /// </summary>
        private ScoreHandler scoreHandler;
        /// <summary>
        /// Holds the information about ailments.
        /// </summary>
        private AilmentHandler ailmentHandler;
        /// <summary>
        /// Holds the information about eating.
        /// </summary>
        private PommesEater pommesEater;

        public SceneObject CurrentCollider { get; private set; }

        private void Start()
        {
            // Initialize member variables
            scoreHandler = GetComponent<ScoreHandler>();
            ailmentHandler = GetComponent<AilmentHandler>();
            pommesEater = GetComponent<PommesEater>();
            // Subscribe to eater delegates
            pommesEater.onStateChange += OnEaterStateChange;
            pommesEater.onDeath += OnEaterDeath;
            pommesEater.onHotSauce += OnHotSauce;
            pommesEater.onPommesEaten += OnPommesEaten;
            CurrentCollider = null;
            // Initialize baits
            Baits = new List<GameObject>();
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
                // Update the score
                scoreHandler.OnActionExecuted();
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
            // Enemy collision shouldn't interupt the player collision with other pickable scene objects
            if (collision.collider.CompareTag("Enemy"))
            {
                return;
            }
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
            // Enemy collision shouldn't interupt the player collision with other pickable scene objects
            if (collision.collider.CompareTag("Enemy"))
            {
                return;
            }
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
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }

        /// <summary>
        /// Called when eaters state has change (Eating -> Starving and vice versa)
        /// </summary>
        private void OnEaterStateChange(EaterState eaterState)
        {
            scoreHandler.OnEaterStateChange(eaterState);
        }

        /// <summary>
        /// Called when eater eats the pommes with hot sauce on it.
        /// </summary>
        private void OnHotSauce()
        {
            if (!IsImmuneTo(AilmentType.Burn))
            {
                ailmentHandler.AddAilment(new BurnAilment());
            }
        }

        /// <summary>
        /// Called when eater eats a pommes.
        /// </summary>
        private void OnPommesEaten()
        {
            scoreHandler.OnPommesEaten();
        }
        #endregion

        #region ICharacter

        public float GetMovementSpeed()
        {
            return MovementSpeed;
        }

        public void SetMovementSpeed(float movementSpeed)
        {
            MovementSpeed = movementSpeed;
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
            int numberOfHotSaucePommes = Mathf.Clamp(PommesBatch.SIZE, 0, pommesEater.HotSaucePommesCapacity);
            // Since you lost some from the capacity but you actually used those with hot sauce as a bait
            // recover the lost pommes in the capacity
            pommesEater.AddPommes(numberOfHotSaucePommes, 0);
            // Remove those from the count
            pommesEater.RemovePommes(PommesBatch.SIZE, numberOfHotSaucePommes);
            return numberOfHotSaucePommes;
        }

        public void InflictWith(Ailment ailment)
        {
            ailmentHandler.AddAilment(ailment);
        }

        public void AddGum()
        {
            pommesEater.AddGum();
        }

        public bool IsImmuneTo(AilmentType ailmentType)
        {
            return ailmentHandler.IsImmune(ailmentType);
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

        public void SetMaxPommesCapacity(int maxCapacity)
        {
            pommesEater.MaxPommesCapacity = maxCapacity;
        }

        public List<Ailment> GetAilments()
        {
            return ailmentHandler.Ailments;
        }

        public float GetEatingRate()
        {
            return pommesEater.EatingRate;
        }

        public void SetEatingRate(float eatingRate)
        {
            pommesEater.EatingRate = eatingRate;
        }

        public float GetDeathClock()
        {
            return pommesEater.DeathClock;
        }

        public void SetDeathClock(float deathClock)
        {
            pommesEater.DeathClock = deathClock;
        }

        public int GetMaxPommesCapacity()
        {
            return pommesEater.MaxPommesCapacity;
        }

        public void AddBait(GameObject bait)
        {
            Baits.Add(bait);
        }

        public void RemoveBait(GameObject bait)
        {
            Baits.Remove(bait);
        }

        public List<GameObject> GetBaits()
        {
            return Baits;
        }

        public int GetCurrentScore()
        {
            return scoreHandler.Score;
        }

        public int GetScore(ScoreType scoreType)
        {
            return ScoreSystem.GetScore(scoreType);
        }

        public int GetPommesCapacity()
        {
            return pommesEater.PommesCapacity;
        }

        public int GetPommesEatenCapacity()
        {
            return pommesEater.PommesEatenCapacity;
        }

        public int GetGumCapacity()
        {
            return pommesEater.GumCapacity;
        }

        public int GetHotSauceCapacity()
        {
            return pommesEater.HotSaucePommesCapacity;
        }
        
        public float GetTimeUntilDeath()
        {
            return pommesEater.GetTimeUntilDeath();
        }
        #endregion
    }
}
