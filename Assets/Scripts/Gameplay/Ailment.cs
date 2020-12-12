using Gameplay.Characters;

namespace Gameplay.Ailments
{
    public abstract partial class Ailment
    {
        /// <summary>
        /// Duration of the ailment.
        /// </summary>
        public float Duration { get; set; }
        /// <summary>
        /// Elapsed time since the ailment has taken effect. Initially zero.
        /// </summary>
        public float ElapsedTime { get; private set; }
        /// <summary>
        /// Determines whether the ailments effect has taken off.
        /// </summary>
        public bool HasFinished => ElapsedTime >= Duration;
        /// <summary>
        /// Activates the ailment properties on the given character.
        /// </summary>
        /// <param name="character">Charcter that gained a new ailment.</param>
        public abstract void Activate(ICharacter character);
    }
}
