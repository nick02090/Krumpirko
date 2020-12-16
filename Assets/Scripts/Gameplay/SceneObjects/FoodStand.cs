using cakeslice;
using Gameplay.Characters;
using UnityEngine;

namespace Gameplay.SceneObjects
{
    public class FoodStand : SceneObject
    {
        public float TimeMultiplier = 2.0f;
        public int BatchOfPommesSize = 5;
        public bool ShowTimer = true;

        private readonly float messageTime = 2.0f;
        private UnityEngine.UI.Text messageText;
        private UnityEngine.UI.Text timerText;
        private Vector3 initialMessagePosition;
        private GameObject pommes;
        private ParticleSystem steam;
        private Outline outline;

        public float TimeUntilNextBatch { get; private set; }
        public int NumberOfPommes { get; private set; }

        private float cookingTimer;
        private float messageTimer;
        private readonly string[] messages = { "Pommes is cooking!", "Almost ready!", "Come back later!" };

        public FoodStand()
        {
            TimeUntilNextBatch = 1.0f;
            NumberOfPommes = 0;
            cookingTimer = 0.0f;
            messageTimer = messageTime;
        }

        private void Start()
        {
            // Get canvas
            Transform canvas = gameObject.transform.GetChild(0);
            // First child of the canvas is the message
            messageText = canvas.GetChild(0).GetComponent<UnityEngine.UI.Text>();
            messageText.gameObject.SetActive(false);
            initialMessagePosition = messageText.transform.position;
            // Second child of the canvas is the timer
            timerText = canvas.GetChild(1).GetComponent<UnityEngine.UI.Text>();

            // Get pommes
            pommes = gameObject.transform.GetChild(1).gameObject;
            pommes.SetActive(false);

            // Get steam particles
            steam = gameObject.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();

            // Get outline
            outline = GetComponent<Outline>();
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
                    // Set the random message as the text
                    messageText.text = messages[index];
                    // Set the text position to initial
                    messageText.transform.position = initialMessagePosition;
                    // Reset the message timer
                    messageTimer = 0.0f;
                }
            }
        }

        public bool HasPommes()
        {
            return NumberOfPommes > 0;
        }

        private void Update()
        {
            // Calculate elapsed time from last frame (seconds)
            cookingTimer += Time.deltaTime;
            messageTimer += Time.deltaTime;

            // Every timeUntilNextBatch seconds put another batch of pommes on the shelves
            if (cookingTimer >= TimeUntilNextBatch)
            {
                // Add new batch of pommes
                NumberOfPommes += BatchOfPommesSize;
                // Increase the timeUntilNextBatch for every new batch
                TimeUntilNextBatch *= TimeMultiplier;
                // Reset the timer
                cookingTimer = 0.0f;
                // Play the steam particle system
                steam.Play();
            }

            // Display the message
            if (messageTimer <= messageTime)
            {
                messageText.gameObject.SetActive(true);
                // Move the message text upwards (semi-static-animation)
                messageText.transform.Translate(messageText.transform.up * Time.deltaTime * 1.0f);
            }
            // Hide the message
            else
            {
                messageText.gameObject.SetActive(false);
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

        public override void ShowOutline()
        {
            outline.eraseRenderer = false;
        }

        public override void HideOutline()
        {
            outline.eraseRenderer = true;
        }
    }
}
