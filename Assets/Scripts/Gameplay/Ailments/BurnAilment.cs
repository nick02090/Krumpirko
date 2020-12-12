using Gameplay.Characters;

namespace Gameplay.Ailments
{
    public abstract partial class Ailment
    {
        public class BurnAilment : Ailment
        {
            /// <summary>
            /// BURN:
            ///     - takes effect after getting your tounge burnt by hot sauce
            ///     - disables eating action
            /// </summary>
            /// <param name="character"></param>
            public override void Activate(ICharacter character)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
