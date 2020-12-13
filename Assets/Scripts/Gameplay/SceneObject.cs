﻿using UnityEngine;

namespace Gameplay.SceneObjects
{
    public abstract class SceneObject : MonoBehaviour
    {
        /// <summary>
        /// Scene object name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Defines interaction with certain the object in the scene.
        /// </summary>
        public abstract void Interact();
    }
}