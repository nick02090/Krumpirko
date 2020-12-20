using Gameplay.Characters;
using UnityEngine;

namespace Gameplay.Ailments
{
    public class FearAilment : Ailment
    {
        private float previousRate = 0.0f;

        public FearAilment() : base()
        {
            Duration = 2.0f;
        }

        /// <summary>
        /// FEAR:
        ///     - takes effect after seeing the player coughing
        ///     - set the eating rate to inf (disables eating)
        /// </summary>
        /// <param name="character"></param>
        public override void Activate(ICharacter character)
        {
            previousRate = character.GetEatingRate();
            character.SetEatingRate(Mathf.Infinity);
        }

        public override AilmentType GetAilmentType()
        {
            return AilmentType.Fear;
        }

        public override Sprite GetSprite()
        {
            return Resources.Load<Sprite>("AilmentSprites/FearAilmentSprite");
        }

        public override void Revert(ICharacter character)
        {
            character.SetEatingRate(previousRate);
        }
    }
}
