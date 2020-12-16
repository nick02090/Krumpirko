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
        public virtual bool IsPickable()
        {
            return false;
        }
    }
}
