using Gameplay.Characters;

namespace Gameplay.Actions
{
    public class KetchupAction : IAction
    {
        /// <summary>
        /// KETCHUP/KECAP:
        ///         - pours ketchup on the floor where the player currently is
        ///         - inflicts Slow ailment to enemies that interact with it
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
