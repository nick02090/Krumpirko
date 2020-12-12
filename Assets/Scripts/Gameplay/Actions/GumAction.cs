namespace Gameplay.Actions
{
    public abstract partial class Action
    {
        public class GumAction : Action
        {
            /// <summary>
            /// GUM/ZVAKA:
            ///         - if activated, the next enemy that tries to steal pommes will get a chewing gum instead
            ///         - inflicts Happy ailment on the enemy
            /// </summary>
            public override void Execute()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
