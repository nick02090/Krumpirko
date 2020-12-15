using Gameplay.Ailments;
using Gameplay.Characters;

namespace Gameplay.SceneObjects
{
    public class Gum : SceneObject
    {
        /// <summary>
        /// Inflicts Happy ailment on the enemy
        /// </summary>
        public override void Interact(ICharacter character)
        {
            if (!character.IsImmuneTo(AilmentType.Happy))
            {
                character.InflictWith(new HappyAilment());
            }
        }
    }
}
