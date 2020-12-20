using UnityEngine;

namespace Core
{
    public class ImagePopup : MonoBehaviour
    {
        public float ImageDuration;
        public bool ShowImage = false;
        public UnityEngine.UI.Image Image;

        private Vector3 initialImagePosition;
        private float timer;

        private void Start()
        {
            // Initialize member variables
            timer = ImageDuration;
            initialImagePosition = Image.transform.position;
            Image.gameObject.SetActive(false);
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (ShowImage)
            {
                if (timer <= ImageDuration)
                {
                    Image.gameObject.SetActive(true);
                    // Move the image upwards (semi-static-animation)
                    Image.transform.Translate(Image.transform.up * Time.deltaTime * 1.0f);
                }
                else
                {
                    Image.gameObject.SetActive(false);
                }
            }
        }

        public void ResetTimer(Sprite sprite)
        {
            Image.sprite = sprite;
            Image.transform.localPosition = initialImagePosition;
            timer = 0.0f;
        }
    }
}
