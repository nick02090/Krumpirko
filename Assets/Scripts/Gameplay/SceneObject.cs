using Gameplay.Characters;
using UnityEngine;

namespace Gameplay.SceneObjects
{
    public abstract class SceneObject : MonoBehaviour
    {
        public ICharacter Creator;

        /// <summary>
        /// Defines interaction with the certain object in the scene.
        /// </summary>
        public abstract void Interact(ICharacter character);
        /// <summary>
        /// Highlight the object if it's pickable
        /// </summary>
        public void ShowHighlight()
        {
            foreach (Material material in GetRenderer().materials)
            {
                material.EnableKeyword("_EMISSION");
            }
        }
        /// <summary>
        /// Disable the highlight of the object.
        /// </summary>
        public void HideHighlight()
        {
            foreach (Material material in GetRenderer().materials)
            {
                material.DisableKeyword("_EMISSION");
            }
        }
        public virtual bool IsPickable()
        {
            return false;
        }
        public virtual MeshRenderer GetRenderer()
        {
            return GetComponent<MeshRenderer>();
        }
    }
}
