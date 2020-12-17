using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Actions
{
    [System.Serializable]
    public class ActionInputs
    {
        public List<KeyCode> BaitInputs = new List<KeyCode>() { KeyCode.Alpha1, KeyCode.Keypad1 };
        public List<KeyCode> CoughInputs = new List<KeyCode>() { KeyCode.Alpha2, KeyCode.Keypad2 };
        public List<KeyCode> GumInputs = new List<KeyCode>() { KeyCode.Alpha3, KeyCode.Keypad3 };
        public List<KeyCode> HotSauceInputs = new List<KeyCode>() { KeyCode.Alpha4, KeyCode.Keypad4 };
        public List<KeyCode> KetchupInputs = new List<KeyCode>() { KeyCode.Alpha5, KeyCode.Keypad5 };

        public bool TryGetInputs(ActionType actionType, out List<KeyCode> inputs)
        {
            switch (actionType)
            {
                case ActionType.Bait:
                    inputs = BaitInputs;
                    return true;
                case ActionType.Cough:
                    inputs = CoughInputs;
                    return true;
                case ActionType.Gum:
                    inputs = GumInputs;
                    return true;
                case ActionType.HotSauce:
                    inputs = HotSauceInputs;
                    return true;
                case ActionType.Ketchup:
                    inputs = KetchupInputs;
                    return true;
                default:
                    inputs = null;
                    return false;
            }
        }
    }
}
