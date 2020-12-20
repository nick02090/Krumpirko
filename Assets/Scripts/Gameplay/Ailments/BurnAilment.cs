using Gameplay.Characters;
using UnityEngine;

namespace Gameplay.Ailments
{
    public class BurnAilment : Ailment
    {
        private int previousCapacity = 0;

        public BurnAilment() : base()
        {
            Duration = 3.0f;
        }

        /// <summary>
        /// BURN:
        ///     - takes effect after getting your tounge burnt by hot sauce
        ///     - sets pommes eater max capacity to zero (disables any more stealing)
        /// </summary>
        /// <param name="character"></param>
        public override void Activate(ICharacter character)
        {
            previousCapacity = character.GetMaxPommesCapacity();
            character.SetMaxPommesCapacity(0);
        }

        public override AilmentType GetAilmentType()
        {
            return AilmentType.Burn;
        }

        public override Sprite GetSprite()
        {
            return Resources.Load<Sprite>("AilmentSprites/BurnAilmentSprite");
        }

        public override void Revert(ICharacter character)
        {
            character.SetMaxPommesCapacity(previousCapacity);
        }
    }
}
