using Control;
using Gameplay.Actions;
using Gameplay.Characters;
using UnityEngine;
using UnityEngine.UI;
using Core;
using System.Collections;

public class HUD : MonoBehaviour
{
    public Text ScoreText;
    public Text ActionText;
    public Text Timer;
    public RectTransform Actions;
    public RectTransform Capacities;

    private PlayerCharacter playerCharacter;
    private PlayerController playerController;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerCharacter = player.GetComponent<PlayerCharacter>();
        playerController = player.GetComponent<PlayerController>();
        playerController.onInvalidAction += OnInvalidAction;
        ActionText.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Update the score
        ScoreText.text = $"Score: {playerCharacter.GetCurrentScore()}";

        // Update available actions
        for (int i = 0; i < Actions.childCount; i++)
        {
            Transform action = Actions.GetChild(i);
            switch (action.name)
            {
                case "Bait":
                    UpdateActionIcon(action, ActionType.Bait);
                    break;
                case "Cough":
                    UpdateActionIcon(action, ActionType.Cough);
                    break;
                case "Gum":
                    UpdateActionIcon(action, ActionType.Gum);
                    break;
                case "HotSauce":
                    UpdateActionIcon(action, ActionType.HotSauce);
                    break;
                case "Ketchup":
                    UpdateActionIcon(action, ActionType.Ketchup);
                    break;
                default:
                    Debug.LogError("HUD::Invalid action name!");
                    break;
            }
        }

        // Update capacities
        for (int i = 0; i < Capacities.childCount; i++)
        {
            Transform capacity = Capacities.GetChild(i);
            switch (capacity.name)
            {
                case "PommesCapacity":
                    UpdateCapacityCounter(capacity, playerCharacter.GetPommesCapacity());
                    break;
                case "PommesEatenCapacity":
                    UpdateCapacityCounter(capacity, playerCharacter.GetPommesEatenCapacity());
                    break;
                case "GumCapacity":
                    UpdateCapacityCounter(capacity, playerCharacter.GetGumCapacity());
                    break;
                case "HotSauceCapacity":
                    UpdateCapacityCounter(capacity, playerCharacter.GetHotSauceCapacity());
                    break;
                default:
                    Debug.LogError("HUD::Invalid capacity name!");
                    break;
            }
        }

        // Update timer
        int remainingTime = (int)Mathf.Ceil(playerCharacter.GetTimeUntilDeath());
        int deathClock = (int)Mathf.Ceil(playerCharacter.GetDeathClock());
        // 70% above the life
        if (remainingTime >= 0.7 * deathClock)
        {
            Timer.color = Color.green;
        }
        // 40% above the life
        else if (remainingTime >= 0.4 * deathClock)
        {
            Timer.color = Color.yellow;
        }
        // below 40% of the life
        else
        {
            Timer.color = Color.red;
        }
        Timer.text = $"{Mathf.Ceil(playerCharacter.GetTimeUntilDeath())}s";

    }

    private void UpdateActionIcon(Transform action, ActionType actionType)
    {
        Image actionImage = action.GetComponent<Image>();
        if (playerCharacter.CanExecuteAction(actionType))
        {
            actionImage.color = new Color(1.0f, 1.0f, 1.0f);
        }
        else
        {
            actionImage.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

    private void UpdateCapacityCounter(Transform capacity, int capacityValue)
    {
        Text capacityText = capacity.GetChild(2).GetComponent<Text>();
        capacityText.text = $"{capacityValue}";
    }

    private IEnumerator OnInvalidAction(ActionType actionType)
    {
        ActionText.gameObject.SetActive(true);
        ActionText.text = $"Unsufficient pommes to execute {actionType.DescriptionAttr()} action!";
        yield return new WaitForSeconds(2);
        ActionText.gameObject.SetActive(false);
    }
}
