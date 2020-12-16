using Gameplay.Ailments;
using Gameplay.Characters;

namespace Gameplay.SceneObjects
{
    public class Cough : SceneObject
    {
        public override void HideOutline()
        {
            throw new System.NotImplementedException();
        }

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

        public override void ShowOutline()
        {
            throw new System.NotImplementedException();
        }
    }
}
