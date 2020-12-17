using Gameplay.Ailments;
using Gameplay.Characters;

namespace Gameplay.SceneObjects
{
    public class Cough : SceneObject
    {
        /// <summary>
        /// Inflicts Fear ailment on the character
        /// </summary>
        public override void Interact(ICharacter character)
        {
            if (!character.IsImmuneTo(AilmentType.Fear))
            {
                character.InflictWith(new FearAilment());
            }
        }
    }
}
