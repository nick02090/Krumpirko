using System.Collections.Generic;

namespace Gameplay.SceneObjects
{
    public class PommesBatch : SceneObject
    {
        /// <summary>
        /// Size of a one batch is always 10 (at start)
        /// </summary>
        public int Size = 10;
        /// <summary>
        /// Number of pommes that have hot sauce over it.
        /// </summary>
        public int HotSauceSize = 0;
        public override void Interact()
        {
            // Check if there's any hot sauce pommes and return it to the collider
            // Update Size and HotSauceSize
            throw new System.NotImplementedException();
        }
    }
}
