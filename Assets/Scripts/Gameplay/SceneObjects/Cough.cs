using Gameplay.Ailments;
using Gameplay.Characters;
using System;
using UnityEngine;

namespace Gameplay.SceneObjects
{
    public class Cough : SceneObject
    {
        public override void Interact()
        {
            throw new NotImplementedException();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                // Get enemy
                EnemyCharacter enemyCharacter = other.GetComponentInParent<EnemyCharacter>();
                // Inflict the enemy with Fear ailment
                enemyCharacter.InflictWith(new FearAilment());
                // Make enemy drop all of its pommes
                enemyCharacter.DropAllPommes();
            }
        }
    }
}
