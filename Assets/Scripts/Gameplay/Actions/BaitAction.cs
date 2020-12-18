using Gameplay.Characters;
using Gameplay.SceneObjects;
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

        public BaitAction(GameObject baitPrefab, Transform baitParent)
        {
            BaitPrefab = baitPrefab;
            BaitParent = baitParent;
        }

        /// <summary>
        /// BAIT/MAMAC:
        ///         - player releases the bait that attracts enemies to it so they don't follow the player
        ///         - releases a certain number of pommes on the floor
        /// </summary>
        public void Execute(ICharacter character)
        {
            // Prepare bait properties
            if (character.CanBaitWithHotSauce())
            {
                PommesBatch pommesBatch = BaitPrefab.GetComponent<PommesBatch>();
                pommesBatch.HotSauceSize = character.BaitWithHotSauce();
            }
            // Get player position
            Transform characterTransform = character.GetTransform();
            // Calculate baits position
            Vector3 baitPosition = characterTransform.position - (characterTransform.up * 0.5f);
            // Create a bait
            GameObject bait = Object.Instantiate(BaitPrefab, baitPosition, characterTransform.rotation);
            // Set the creator of the bait
            bait.GetComponent<PommesBatch>().Creator = character;
            character.AddBait(bait);
            // Set bait in the list of baits
            bait.transform.SetParent(BaitParent);
        }

        public ActionCostType GetCostType()
        {
            return ActionCostType.PommesCapacity;
        }
    }
}
