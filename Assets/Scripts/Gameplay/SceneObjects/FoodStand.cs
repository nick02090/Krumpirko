using Core;
using Gameplay.Characters;
using UnityEngine;

namespace Gameplay.SceneObjects
{
    public class FoodStand : SceneObject
    {
        public float TimeMultiplier = 2.0f;
        public int BatchOfPommesSize = 5;
        public bool ShowTimer = true;
        public float MaxTimeUntilNextBatch = 99.0f;

        public MessagePopup MessagePopup;

        public GameObject modelPrefab;

        private UnityEngine.UI.Text timerText;
        private GameObject pommes;
        private ParticleSystem steam;

        public float TimeUntilNextBatch { get; private set; }
        public int NumberOfPommes { get; private set; }

        private float cookingTimer;
        private readonly string[] messages = { "Pommes is cooking!", "Almost ready!", "Come back later!",
                                               "It's currently Irish Famine in the kitchen :(",
                                               "Pommes doesn't grow on trees, you know"};

        public FoodStand()
        {
            TimeUntilNextBatch = 1.0f;
            NumberOfPommes = 0;
            cookingTimer = 0.0f;
        }

        private void Start()
        {
            // Set the message popup to always show the message
            MessagePopup.ShowMessage = true;

            // Get canvas
            Transform canvas = gameObject.transform.GetChild(0);
            // First child of the canvas is the timer
            timerText = canvas.GetChild(0).GetComponent<UnityEngine.UI.Text>();

            // Get pommes
            pommes = gameObject.transform.GetChild(1).gameObject;
            pommes.SetActive(false);

            // Get steam particles
            steam = gameObject.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
        }

        /// <summary>
        /// Adds extra pommes to the character capacity.
        /// </summary>
        /// <param name="character"></param>
        public override void Interact(ICharacter character)
        {
            if (character.HasTag("Player"))
            {
                if (HasPommes())
                {
                    int remainingPommes = character.GetLeftPommesCapacity();
                    // Calculate the total number of pommes that the player will recieve
                    int numberOfPommes = Mathf.Clamp(remainingPommes, 0, NumberOfPommes);
                    character.AddPommes(numberOfPommes, 0);
                    // Update pommes numbers inside this batch
                    NumberOfPommes -= numberOfPommes;
                }
                else
                {
                    // Select a random message
                    System.Random random = new System.Random();
                    int index = random.Next(0, messages.Length);
                    MessagePopup.ResetTimer(messages[index], null);
                }
            }
        }

        public bool HasPommes()
        {
            return NumberOfPommes > 0;
        }

        private void FixedUpdate()
        {
            // Calculate elapsed time from last frame (seconds)
            cookingTimer += Time.deltaTime;

            // Every timeUntilNextBatch seconds put another batch of pommes on the shelves
            if (cookingTimer >= TimeUntilNextBatch)
            {
                // Add new batch of pommes
                NumberOfPommes += BatchOfPommesSize;
                // Increase the timeUntilNextBatch for every new batch
                TimeUntilNextBatch = Mathf.Clamp(TimeUntilNextBatch * TimeMultiplier, TimeUntilNextBatch, MaxTimeUntilNextBatch);
                // Reset the timer
                cookingTimer = 0.0f;
                // Play the steam particle system
                steam.Play();
            }

            // Display the timer on the food stand
            timerText.gameObject.SetActive(ShowTimer);
            timerText.text = $"Next in {Mathf.Ceil(TimeUntilNextBatch - cookingTimer)}";

            // Show pommes batch if theres any pommes in the food stand
            pommes.SetActive(HasPommes());
        }

        public override bool IsPickable()
        {
            return true;
        }

        public override MeshRenderer GetRenderer()
        {
            return modelPrefab.GetComponent<MeshRenderer>();
        }
    }
}
