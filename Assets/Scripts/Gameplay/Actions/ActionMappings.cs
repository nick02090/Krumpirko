using UnityEngine;

namespace Gameplay.Actions
{
    [System.Serializable]
    public class ActionMappings
    {
        public GameObject BaitPrefab;
        public Transform BaitParent;

        public ParticleSystem CoughParticle;
        public Transform CoughParent;

        public GameObject KetchupPrefab;
        public Transform KetchupParent;

        public bool TryGetAction(ActionType actionType, out IAction action)
        {
            switch (actionType)
            {
                case ActionType.Bait:
                    action = new BaitAction(BaitPrefab, BaitParent);
                    return true;
                case ActionType.Cough:
                    action = new CoughAction(CoughParticle, CoughParent);
                    return true;
                case ActionType.Gum:
                    action = new GumAction();
                    return true;
                case ActionType.HotSauce:
                    action = new HotSauceAction();
                    return true;
                case ActionType.Ketchup:
                    action = new KetchupAction(KetchupPrefab, KetchupParent);
                    return true;
                default:
                    action = null;
                    return false;
            }
        }
    }
}
