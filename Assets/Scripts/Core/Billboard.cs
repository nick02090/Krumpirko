using UnityEngine;

namespace Core
{
    public enum BillBoardType
    {
        Text,
        Sprite
    };

    public class Billboard : MonoBehaviour
    {
        public BillBoardType type;

        private void LateUpdate()
        {
            switch(type)
            {
                case BillBoardType.Sprite:
                    transform.LookAt(Camera.main.transform.position, Vector3.up);
                    break;
                case BillBoardType.Text:
                    transform.forward = new Vector3(Camera.main.transform.forward.x, transform.forward.y, Camera.main.transform.forward.z);
                    break;
                default:
                    Debug.LogError("Invalid billboard type!");
                    break;
            }
        }
    }
}
