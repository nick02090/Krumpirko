using Gameplay.Characters;
using UnityEngine;

namespace Gameplay.SceneObjects
{
    public class FoodStand : SceneObject
    {
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
                }
            }
        }
        public bool HasPommes()
        {
            // TODO: Implement this shiiiet
            return false;
        }
        private void Update()
        {
            // Every X seconds put another batch of pommes on the shelves
            // Increase the X every time
        }
    }
}
