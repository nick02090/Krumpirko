namespace Gameplay.Actions
{
    [System.Serializable]
    public class ActionCosts
    {
        public int BaitActionCost = 10;
        public int CoughActionCost = 20;
        public int GumActionCost = 5;
        public int HotSauceActionCost = 30;
        public int KetchupActionCost = 15;

        public float ActionMultiplier = 1.0f;

        public int GetCost(ActionType actionType)
        {
            switch(actionType)
            {
                case ActionType.Bait:
                    return (int)(BaitActionCost * ActionMultiplier);
                case ActionType.Cough:
                    return (int)(CoughActionCost * ActionMultiplier);
                case ActionType.Gum:
                    return (int)(GumActionCost * ActionMultiplier);
                case ActionType.HotSauce:
                    return (int)(HotSauceActionCost * ActionMultiplier);
                case ActionType.Ketchup:
                    return (int)(KetchupActionCost * ActionMultiplier);
                default:
                    return 0;
            }
        }
    }
}
