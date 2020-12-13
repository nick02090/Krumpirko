using Gameplay.Ailments;
using Gameplay.Characters;
using UnityEngine;

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
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                // Get enemy
                EnemyCharacter enemyCharacter = other.GetComponentInParent<EnemyCharacter>();
                // Inflict the enemy with Slow ailment
                enemyCharacter.InflictWith(new SlowAilment());
            }
        }
    }
}
