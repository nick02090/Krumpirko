using Gameplay.Characters;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Ailments
{
    public class AilmentHandler : MonoBehaviour
    {
        public delegate void OnAilmentInflict(Ailment ailment);
        public OnAilmentInflict onAilmentInflict;

        public AilmentImmunity AilmentImmunity;

        public List<Ailment> Ailments { get; private set; }

        private ICharacter character;


        private void Start()
        {
            character = GetComponent<ICharacter>();
            Ailments = new List<Ailment>();
        }

        private void Update()
        {
            for (int i = Ailments.Count - 1; i >= 0; i--)
            {
                Ailments[i].Update(Time.deltaTime);
                if (Ailments[i].HasFinished)
                {
                    Ailments[i].Revert(character);
                    Ailments.RemoveAt(i);
                }
            }
        }

        public void AddAilment(Ailment ailment)
        {
            if (Ailments.FirstOrDefault(infectedAilment => infectedAilment.GetAilmentType() == ailment.GetAilmentType()) == null
                && !IsImmune(ailment.GetAilmentType()))
            {
                ailment.Activate(character);
                Ailments.Add(ailment);
                onAilmentInflict?.Invoke(ailment);
            }
        }

        public bool IsImmune(AilmentType ailmentType)
        {
            return AilmentImmunity.GetImmunity(ailmentType);
        }
    }
}
