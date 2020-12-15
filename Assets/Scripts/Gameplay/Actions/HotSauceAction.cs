using Gameplay.Characters;

namespace Gameplay.Actions
{
    public class HotSauceAction : IAction
    {
        /// <summary>
        /// HOT-SAUCE/LJUTI-UMAK:
        ///         - spills hot sauce over dish where pommes are
        ///         - effective for a certain number of pommes that are in the dish or will be put in the dish later on
        ///         - infects Burn ailment to enemies that eat those pommes
        /// </summary>
        public void Execute(ICharacter character)
        {
            // Spill hot sauce over characters pommes
            character.SpillHotSauce();
        }

        public ActionCostType GetCostType()
        {
            return ActionCostType.PommesCapacity;
        }
    }
}
