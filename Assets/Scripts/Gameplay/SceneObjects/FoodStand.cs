using Gameplay.Characters;
using UnityEngine;

namespace Gameplay.SceneObjects
{
    public class FoodStand : SceneObject
    {
        public float TimeMultiplier = 2.0f;
        public int BatchOfPommesSize = 5;

        public float timeUntilNextBatch = 1.0f; // TODO: Set to public once you finish debugging
        public int numberOfPommes = 0; // TODO: Set to private once you finish debugging

        private float timer = 0.0f;

        /// <summary>
        /// Adds extra pommes to the character capacity.
        /// </summary>
        /// <param name="character"></param>
        public override void Interact(ICharacter character)
        {
            if (HasPommes())
            {
                if (character.HasTag("Player"))
                {
                    int remainingPommes = character.GetLeftPommesCapacity();
                    // Calculate the total number of pommes that the player will recieve
                    int numberOfPommes = Mathf.Clamp(remainingPommes, 0, this.numberOfPommes);
                    character.AddPommes(numberOfPommes, 0);
                    // Update pommes numbers inside this batch
                    this.numberOfPommes -= numberOfPommes;
                }
            }
            else
            {
                Debug.Log("Pommes is cooking! Come back later!");
            }
        }

        public bool HasPommes()
        {
            return numberOfPommes > 0;
        }

        private void Update()
        {
            // Calculate elapsed time from last frame (seconds)
            timer += Time.deltaTime;

            // Every timeUntilNextBatch seconds put another batch of pommes on the shelves
            if (timer >= timeUntilNextBatch)
            {
                // Add new batch of pommes
                numberOfPommes += BatchOfPommesSize;
                // Increase the timeUntilNextBatch for every new batch
                timeUntilNextBatch *= TimeMultiplier;
                // Reset the timer
                timer = 0.0f;
            }
        }

        public override bool IsPickable()
        {
            return true;
        }
    }
}
