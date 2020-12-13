using Gameplay.Characters;
using UnityEngine;

namespace Gameplay.Actions
{
    public class KetchupAction : IAction
    {
        /// <summary>
        /// Game object that will be used as a ketchup.
        /// </summary>
        public GameObject KetchupPrefab { get; private set; }

        /// <summary>
        /// Transform that will use as a ketchup's parent.
        /// </summary>
        public Transform KetchupParent { get; private set; }

        public KetchupAction(GameObject ketchupPrefab, Transform ketchupParent)
        {
            KetchupPrefab = ketchupPrefab;
            KetchupParent = ketchupParent;
        }

        /// <summary>
        /// KETCHUP/KECAP:
        ///         - pours ketchup on the floor where the player currently is
        ///         - inflicts Slow ailment to enemies that interact with it
        /// </summary>
        public void Execute(ICharacter character)
        {
            // Get player position
            Transform characterTransform = character.GetTransform();
            // Calculate ketchups position (in front of the player)
            Vector3 ketchupPosition = characterTransform.position - (characterTransform.up * 0.9f);
            // Create a ketchup
            GameObject ketchup = Object.Instantiate(KetchupPrefab, ketchupPosition, characterTransform.rotation);
            // Set ketchup in the list of ketchups
            ketchup.transform.SetParent(KetchupParent);
        }

        public ActionCostType GetCostType()
        {
            return ActionCostType.PommesEaten;
        }
    }
}
