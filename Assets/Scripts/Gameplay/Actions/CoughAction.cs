using Gameplay.Characters;
using UnityEngine;

namespace Gameplay.Actions
{
    public class CoughAction : IAction
    {
        /// <summary>
        /// Particle effect that will be used as a cough cloud.
        /// </summary>
        public ParticleSystem CoughParticle { get; private set; }

        /// <summary>
        /// Transform that will use as a cough's parent.
        /// </summary>
        public Transform CoughParent { get; private set; }

        public CoughAction(ParticleSystem coughParticle, Transform coughParent)
        {
            CoughParticle = coughParticle;
            CoughParent = coughParent;
        }

        /// <summary>
        /// COUGH/KASALJ:
        ///         - affects all enemies that are within the radius of the players current position
        ///         - enemies are inflicted with Fear ailment and they immediately drop all of their pommes
        /// </summary>
        public void Execute(ICharacter character)
        {
            // Get character position
            Transform characterTransform = character.GetTransform();
            // Position a cough particle around a character
            CoughParticle.transform.position = characterTransform.position;
            // Create a particle
            GameObject.Instantiate(CoughParticle);
            // Play dust/cloud-like particle effect
            CoughParticle.Play();
        }

        public ActionCostType GetCostType()
        {
            return ActionCostType.PommesEaten;
        }
    }
}
