namespace Gameplay.Actions
{
    public abstract partial class Action
    {
        public class BaitAction : Action
        {
            /// <summary>
            /// BAIT/MAMAC:
            ///         - player releases the bait that attracts enemies to it so they don't follow the player
            ///         - releases a certain number of pommes on the floor
            /// </summary>
            public override void Execute()
            {
                throw new System.NotImplementedException();
            }
        }

    }
}
