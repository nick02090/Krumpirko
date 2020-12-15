using Gameplay.Ailments;
using Gameplay.Characters;

namespace Gameplay.SceneObjects
{
    public class Ketchup : SceneObject
    {
        /// <summary>
        /// Determines for how many more enemies is effective.
        /// </summary>
        public int EffectiveFor = 3;
        /// <summary>
        /// Inflicts Slow ailment on the character
        /// </summary>
        public override void Interact(ICharacter character)
        {
            if (!character.IsImmuneTo(AilmentType.Slow))
            {
                character.InflictWith(new SlowAilment());
                EffectiveFor--;
            }
        }
        private void Update()
        {
            if (EffectiveFor <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
