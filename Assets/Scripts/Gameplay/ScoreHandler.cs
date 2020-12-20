using Gameplay.Characters;
using System.ComponentModel;
using UnityEngine;

namespace Gameplay
{
    public enum ScoreType
    {
        [Description("Eaten pommes")]
        EatenPommes,
        [Description("Action executed")]
        ActionExecuted,
        [Description("Second of life")]
        SecondOfLife,
        [Description("Death avoided")]
        DeathAvoided
    }

    public class ScoreHandler : MonoBehaviour
    {
        public int Score { get; private set; }

        private ICharacter character;
        private float timer = 0.0f;
        private float OneSecond = 1.0f;

        private void Start()
        {
            character = GetComponent<ICharacter>();
            Score = 0;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= OneSecond)
            {
                // Update the score
                Score += character.GetScore(ScoreType.SecondOfLife);
                // Reset the timer
                timer = 0.0f;
            }
        }

        public void OnEaterStateChange(EaterState eaterState)
        {
            // eaterState holds the new state.
            // If the current state is Eating then the previous one must've been Starving.
            if (eaterState == EaterState.Eating)
            {
                Score += character.GetScore(ScoreType.DeathAvoided);
            }
        }

        public void OnPommesEaten()
        {
            Score += character.GetScore(ScoreType.EatenPommes);
        }

        public void OnActionExecuted()
        {
            Score += character.GetScore(ScoreType.ActionExecuted);
        }
    }
}
