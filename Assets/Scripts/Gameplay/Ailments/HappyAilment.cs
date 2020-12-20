using Gameplay.Characters;
using UnityEngine;

namespace Gameplay.Ailments
{
    public class HappyAilment : Ailment
    {
        public HappyAilment() : base()
        {
            // This doesn't matter since the infection is permanent
            Duration = 0.0f;
        }

        /// <summary>
        /// HAPPY:
        ///     - takes effect after eating a chewing gum
        ///     - swallowing a chewing gum reduces your life
        /// </summary>
        /// <param name="character"></param>
        public override void Activate(ICharacter character)
        {
            float deathClock = Mathf.Clamp(character.GetDeathClock() * 0.8f, 1.0f, character.GetDeathClock());
            character.SetDeathClock(deathClock);
        }

        public override AilmentType GetAilmentType()
        {
            return AilmentType.Happy;
        }

        public override Sprite GetSprite()
        {
            return Resources.Load<Sprite>("AilmentSprites/HappyAilmentSprite");
        }

        public override void Revert(ICharacter character)
        {
            // Muahaha, nothing happens!
            // Don't eat gums kids!
        }
    }
}
