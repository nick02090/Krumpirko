using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Characters;
using Gameplay.Actions;
using Core;

namespace Control
{
    [RequireComponent(typeof(PlayerCharacter))]
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// Holds the information about the player.
        /// </summary>
        private PlayerCharacter playerCharacter;
        /// <summary>
        /// Action mapping with key codes for the input.
        /// </summary>
        private Dictionary<ActionType, List<KeyCode>> actionMapping = new Dictionary<ActionType, List<KeyCode>>()
        {
            { ActionType.Bait, new List<KeyCode>(){ KeyCode.Alpha1, KeyCode.Keypad1 } },
            { ActionType.Cough, new List<KeyCode>(){ KeyCode.Alpha2, KeyCode.Keypad2 } },
            { ActionType.Gum, new List<KeyCode>(){ KeyCode.Alpha3, KeyCode.Keypad3 } },
            { ActionType.HotSauce, new List<KeyCode>(){ KeyCode.Alpha4, KeyCode.Keypad4 } },
            { ActionType.Ketchup, new List<KeyCode>(){ KeyCode.Alpha5, KeyCode.Keypad5 } },
        };

        void Start()
        {
            // Initialize member variables
            playerCharacter = GetComponent<PlayerCharacter>();
        }

        void Update()
        {
            // Move player (based on input)
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float movementSpeed = playerCharacter.GetMovementSpeed();
            transform.Translate(movementSpeed * horizontal * Time.deltaTime, 0f, movementSpeed * vertical * Time.deltaTime);

            // Check for input and play an action if available
            foreach (KeyValuePair<ActionType, List<KeyCode>> keyValuePair in actionMapping)
            {
                ActionType actionType = keyValuePair.Key;
                List<KeyCode> keyCodes = keyValuePair.Value;
                foreach (KeyCode keyCode in keyCodes)
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        // Player can't execute this action due to unsuficient pommes
                        if (!playerCharacter.CanExecuteAction(actionType))
                        {
                            // TODO: Print a warning message on screen that this action is not available!
                            Debug.LogWarning($"{actionType.DescriptionAttr()} can't be executed due to unsufficient pommes");
                            break;
                        }
                        else
                        {
                            playerCharacter.ExecuteAction(actionType);
                        }
                    }
                }
            }
        }
    }
}
