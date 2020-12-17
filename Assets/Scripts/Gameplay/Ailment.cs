using Gameplay.Characters;
using System.ComponentModel;

namespace Gameplay.Ailments
{
    public enum AilmentType
    {
        [Description("Burn")]
        Burn,
        [Description("Fear")]
        Fear,
        [Description("Happy")]
        Happy,
        [Description("Slow")]
        Slow
    }

    public abstract class Ailment
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

        public Ailment()
        {
            ElapsedTime = 0.0f;
        }

        public abstract AilmentType GetAilmentType();
        
        /// <summary>
        /// Activates the ailment properties on the given character.
        /// </summary>
        /// <param name="character">Charcter that gained a new ailment.</param>
        public abstract void Activate(ICharacter character);
        public abstract void Revert(ICharacter character);
        public void Update(float deltaTime)
        {
            ElapsedTime += deltaTime;
        }
    }
}
