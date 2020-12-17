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

        public override void HideOutline()
        {
            throw new System.NotImplementedException();
        }

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

        public override void ShowOutline()
        {
            throw new System.NotImplementedException();
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
