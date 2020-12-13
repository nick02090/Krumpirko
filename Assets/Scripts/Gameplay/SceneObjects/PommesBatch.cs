using System.Collections.Generic;

namespace Gameplay.SceneObjects
{
    public class PommesBatch : SceneObject
    {
        public List<Pommes> Pommes { get; private set; }
        public override void Interact()
        {
            throw new System.NotImplementedException();
        }
    }
}
