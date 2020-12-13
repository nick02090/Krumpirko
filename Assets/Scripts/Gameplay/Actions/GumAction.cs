using Gameplay.Characters;

namespace Gameplay.Actions
{
    public class GumAction : IAction
    {
        /// <summary>
        /// GUM/ZVAKA:
        ///         - if activated, the next enemy that tries to steal pommes will get a chewing gum instead
        ///         - inflicts Happy ailment on the enemy
        /// </summary>
        public void Execute(ICharacter character)
        {
            throw new System.NotImplementedException();
        }

        public int GetCost()
        {
            throw new System.NotImplementedException();
        }

        ActionType IAction.GetType()
        {
            throw new System.NotImplementedException();
        }
    }
}
