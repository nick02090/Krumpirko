﻿using Gameplay.Characters;

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
            character.AddGum();
        }

        public ActionCostType GetCostType()
        {
            return ActionCostType.PommesEaten;
        }
    }
}
