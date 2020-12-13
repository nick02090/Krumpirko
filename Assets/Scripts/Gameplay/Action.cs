using Gameplay.Characters;
using System.ComponentModel;

namespace Gameplay.Actions
{
    public enum ActionType
    {
        [Description("Bait")]
        Bait,
        [Description("Cough")]
        Cough,
        [Description("Gum")]
        Gum,
        [Description("Hot Sauce")]
        HotSauce,
        [Description("Ketchup")]
        Ketchup
    }

    public interface IAction
    {
        /// <summary>
        /// Gets cost for this action.
        /// </summary>
        /// <returns></returns>
        int GetCost();
        /// <summary>
        /// Gets type of this action.
        /// </summary>
        /// <returns></returns>
        ActionType GetType();
        /// <summary>
        /// Activates this action.
        /// </summary>
        void Execute(ICharacter character);
    }
}
