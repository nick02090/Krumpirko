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

    public enum ActionCostType
    {
        [Description("Pommes Capacity")]
        PommesCapacity,
        [Description("Pommes Eaten")]
        PommesEaten
    }

    public interface IAction
    {
        /// <summary>
        /// Gets type of the cost for this action.
        /// PommesCapacity means that action is calculated based on the number of pommes character has at disposal.
        /// PommesEaten means that action is calculated based on the number of pommes character has eaten so far in the game.
        /// </summary>
        /// <returns></returns>
        ActionCostType GetCostType();
        /// <summary>
        /// Activates this action.
        /// </summary>
        void Execute(ICharacter character);
    }
}
