using Gameplay.Characters;

namespace Gameplay.Ailments
{
    public class SlowAilment : Ailment
    {
        private float previousSpeed = 0.0f;

        public SlowAilment() : base()
        {
            Duration = 5.0f;
        }

        /// <summary>
        /// SLOW:
        ///     - takes effect after stepping into a ketchup
        ///     - slows down movement speed
        /// </summary>
        /// <param name="character"></param>
        public override void Activate(ICharacter character)
        {
            float currentSpeed = character.GetMovementSpeed();
            previousSpeed = currentSpeed;
            character.SetMovementSpeed(currentSpeed / 2.0f);
        }

        public override AilmentType GetAilmentType()
        {
            return AilmentType.Slow;
        }

        public override void Revert(ICharacter character)
        {
            character.SetMovementSpeed(previousSpeed);
        }
    }
}
