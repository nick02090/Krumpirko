using Gameplay.Characters;
using UnityEngine;

namespace Gameplay.Actions
{
    public class BaitAction : IAction
    {
        /// <summary>
        /// Game object that will be used as a bait.
        /// </summary>
        public GameObject BaitPrefab { get; private set; }

        /// <summary>
        /// Transform that will use as a bait's parent.
        /// </summary>
        public Transform BaitParent { get; private set; }

        private readonly int _cost;

        public BaitAction(GameObject baitPrefab, Transform baitParent, int cost = 100)
        {
            BaitPrefab = baitPrefab;
            BaitParent = baitParent;
            _cost = cost;
        }

        /// <summary>
        /// BAIT/MAMAC:
        ///         - player releases the bait that attracts enemies to it so they don't follow the player
        ///         - releases a certain number of pommes on the floor
        /// </summary>
        public void Execute(ICharacter character)
        {
            // Get player position
            Transform characterTransform = character.GetTransform();
            // Calculate baits position (in front of the player)
            Vector3 baitPosition = characterTransform.position - (characterTransform.up * 0.5f);
            // Create a bait
            GameObject bait = Object.Instantiate(BaitPrefab, baitPosition, characterTransform.rotation);
            // Set bait in the list of baits
            bait.transform.SetParent(BaitParent);
        }

        public int GetCost()
        {
            // TODO: Redo this so the action cost is read from an outside script (and/or can be changed via editor).
            return _cost;
        }

        ActionType IAction.GetType()
        {
            return ActionType.Bait;
        }
    }
}
