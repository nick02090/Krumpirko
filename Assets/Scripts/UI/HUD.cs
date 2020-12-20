using Gameplay.Actions;
using Gameplay.Characters;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text ScoreText;
    public RectTransform Actions;

    private PlayerCharacter playerCharacter;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerCharacter = player.GetComponent<PlayerCharacter>();
    }

    private void Update()
    {
        ScoreText.text = $"Score: {playerCharacter.GetCurrentScore()}";

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
}
