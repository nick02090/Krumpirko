using Gameplay.Characters;
using UnityEngine;

namespace Gameplay.SceneObjects
{
    public abstract class SceneObject : MonoBehaviour
    {
        /// <summary>
        /// Defines interaction with the certain object in the scene.
        /// </summary>
        public abstract void Interact(ICharacter character);
        /// <summary>
        /// Outlines the object if it's pickable
        /// </summary>
        public abstract void ShowOutline();
        /// <summary>
        /// Hides the outline of the object.
        /// </summary>
        public abstract void HideOutline();
        public virtual bool IsPickable()
        {
            return false;
        }
    }
}
