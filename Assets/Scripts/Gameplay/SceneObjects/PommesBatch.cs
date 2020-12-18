using Gameplay.Characters;
using UnityEngine;

namespace Gameplay.SceneObjects
{
    public class PommesBatch : SceneObject
    {
        public const int SIZE = 10;

        /// <summary>
        /// Size of a one batch is always 10 (at start)
        /// </summary>
        public int Size = SIZE;
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
                // NOTE: This is possible due to the fact that the HotSauceSize is always less or equal to Size
                character.AddPommes(numberOfPommes, numberOfHotPommes);
                // Update pommes numbers inside this batch
                Size -= numberOfPommes;
                HotSauceSize -= numberOfHotPommes;
            }
        }

        public override bool IsPickable()
        {
            return true;
        }

        private void Update()
        {
            if (Size <= 0)
            {
                Creator.RemoveBait(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
