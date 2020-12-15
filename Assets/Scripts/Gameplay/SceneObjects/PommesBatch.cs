using Gameplay.Characters;
using UnityEngine;

namespace Gameplay.SceneObjects
{
    public class PommesBatch : SceneObject
    {
        /// <summary>
        /// Size of a one batch is always 10 (at start)
        /// </summary>
        public int Size = 10;
        /// <summary>
        /// Number of pommes that have hot sauce over it.
        /// </summary>
        public int HotSauceSize = 0;
        public override void Interact(ICharacter character)
        {
            if (character.HasTag("Enemy"))
            {
                int remainingPommes = character.GetLeftPommesCapacity();
                // Calculate the total number of pommes and the number of hot pommes inside that total
                int numberOfPommes = Mathf.Clamp(remainingPommes, 0, Size);
                int numberOfHotPommes = Mathf.Clamp(remainingPommes, 0, HotSauceSize);
                character.AddPommes(numberOfPommes, numberOfHotPommes);
                // Update pommes numbers inside this batch
                Size -= numberOfPommes;
                HotSauceSize -= numberOfHotPommes;
            }
        }
        private void Update()
        {
            if (Size <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
