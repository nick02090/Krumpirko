namespace Gameplay.SceneObjects
{
    public class Ketchup : SceneObject
    {
        /// <summary>
        /// Determines for how many more enemies is effective.
        /// </summary>
        public int EffectiveFor { get; set; }
        public override void Interact()
        {
            throw new System.NotImplementedException();
        }
    }
}
