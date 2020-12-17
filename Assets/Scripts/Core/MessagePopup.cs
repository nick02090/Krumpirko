using UnityEngine;

namespace Core
{
    public class MessagePopup : MonoBehaviour
    {
        public float MessageDuration;
        public bool ShowMessage = false;
        public UnityEngine.UI.Text MessageText;

        private Vector3 initialMessagePosition;
        private float timer;
        private Color defaultWhiteColor = Color.white;

        private void Start()
        {
            // Initialize member variables
            timer = MessageDuration;
            initialMessagePosition = MessageText.transform.position;
            MessageText.gameObject.SetActive(false);
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (ShowMessage)
            {
                if (timer <= MessageDuration)
                {
                    MessageText.gameObject.SetActive(true);
                    // Move the message text upwards (semi-static-animation)
                    MessageText.transform.Translate(MessageText.transform.up * Time.deltaTime * 1.0f);
                }
                else
                {
                    MessageText.gameObject.SetActive(false);
                }
            }
        }

        public void ResetTimer(string message, Color? messageColor)
        {
            MessageText.text = message;
            MessageText.color = messageColor ?? defaultWhiteColor;
            MessageText.transform.localPosition = initialMessagePosition;
            timer = 0.0f;
        }
    }
}
