using Gameplay.Characters;

namespace Gameplay.Actions
{
    public class CoughAction : IAction
    {
        /// <summary>
        /// COUGH/KASALJ:
        ///         - affects all enemies that are within the radius of the players current position
        ///         - enemies are inflicted with Fear ailment and they immediately drop all of their pommes
        /// </summary>
        public void Execute(ICharacter character)
        {
            throw new System.NotImplementedException();
        }

        public ActionCostType GetCostType()
        {
            return ActionCostType.PommesEaten;
        }
    }
}
