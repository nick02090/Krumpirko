using Gameplay.Characters;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public UnityEngine.UI.Text ScoreText;
    public RectTransform Actions;

    private ICharacter playerCharacter;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerCharacter = player.GetComponent<PlayerCharacter>();
    }

    private void Update()
    {
        ScoreText.text = $"Score: {playerCharacter.GetCurrentScore()}";
    }
}
