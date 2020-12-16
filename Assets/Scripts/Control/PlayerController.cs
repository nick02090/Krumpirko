﻿using System.Collections.Generic;
using UnityEngine;
using Gameplay.Characters;
using Gameplay.Actions;
using Core;
using Gameplay.SceneObjects;

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
        private readonly Dictionary<ActionType, List<KeyCode>> actionMapping = new Dictionary<ActionType, List<KeyCode>>()
        {
            { ActionType.Bait, new List<KeyCode>(){ KeyCode.Alpha1, KeyCode.Keypad1 } },
            { ActionType.Cough, new List<KeyCode>(){ KeyCode.Alpha2, KeyCode.Keypad2 } },
            { ActionType.Gum, new List<KeyCode>(){ KeyCode.Alpha3, KeyCode.Keypad3 } },
            { ActionType.HotSauce, new List<KeyCode>(){ KeyCode.Alpha4, KeyCode.Keypad4 } },
            { ActionType.Ketchup, new List<KeyCode>(){ KeyCode.Alpha5, KeyCode.Keypad5 } },
        };

        private void Start()
        {
            // Initialize member variables
            playerCharacter = GetComponent<PlayerCharacter>();
        }

        private void FixedUpdate()
        {
            // Move player (based on input)
            float vertical = Input.GetAxis("Vertical");
            float movementSpeed = playerCharacter.GetMovementSpeed();
            transform.Translate(0f, 0f, movementSpeed * vertical * Time.deltaTime);
            float horizontal = Input.GetAxis("Horizontal");
            float rotationSpeed = playerCharacter.GetRotationSpeed();
            transform.Rotate(transform.up, rotationSpeed * horizontal * Time.deltaTime);
        }

        private void Update()
        {
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

            // Check for pickup input
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // If the player is currently colliding with anything
                if (playerCharacter.TryGetCollider(out SceneObject sceneObject))
                {
                    // Make player interact with it
                    sceneObject.Interact(playerCharacter);
                }
            }
        }
    }
}