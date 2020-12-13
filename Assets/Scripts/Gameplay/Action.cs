namespace Gameplay.Actions
{
    public abstract class Action
    {
        /// <summary>
        /// Cost of the action.
        /// </summary>
        public const int cost = 0;
        /// <summary>
        /// Activates this action.
        /// </summary>
        public abstract void Execute();
    }
}
