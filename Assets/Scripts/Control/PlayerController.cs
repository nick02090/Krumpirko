using System.Collections.Generic;
using UnityEngine;
using Gameplay.Characters;
using Gameplay.Actions;
using Gameplay.SceneObjects;
using System.Collections;

namespace Control
{
    [RequireComponent(typeof(PlayerCharacter))]
    public class PlayerController : MonoBehaviour
    {
        public delegate IEnumerator InvalidAction(ActionType actionType);
        public InvalidAction onInvalidAction;

        /// <summary>
        /// Holds the information about the player.
        /// </summary>
        private PlayerCharacter playerCharacter;
        /// <summary>
        /// Action mapping with key codes for the input.
        /// </summary>
        public ActionInputs ActionInputs;
        public KeyCode PickupInput = KeyCode.Space;
        public List<ActionType> AvailableActions = new List<ActionType>() 
        { 
            ActionType.Bait, 
            ActionType.Cough, 
            ActionType.Gum, 
            ActionType.HotSauce, 
            ActionType.Ketchup 
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
            foreach (ActionType actionType in AvailableActions)
            {
                if (ActionInputs.TryGetInputs(actionType, out List<KeyCode> keyCodes))
                {
                    foreach (KeyCode keyCode in keyCodes)
                    {
                        if (Input.GetKeyDown(keyCode))
                        {
                            // Player can't execute this action due to unsuficient pommes
                            if (!playerCharacter.CanExecuteAction(actionType))
                            {
                                // Invoke the subscribers when action can't be executed
                                StartCoroutine(onInvalidAction?.Invoke(actionType));
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

            // Check for pickup input
            if (Input.GetKeyDown(PickupInput))
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
