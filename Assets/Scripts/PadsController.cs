using UnityEngine;

namespace BrickBreaker
{
    public class PadsController : MonoBehaviour
    {
        public Pad[] pads;
        public float minX;
        public float maxX;
        public float moveSpeed;

        private void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                MovePads(1);
            }

            if (Input.GetKey(KeyCode.D))
            {
                MovePads(-1);
            }
        }

        private void MovePads(int directionSign)
        {
            for (int i = 0; i < pads.Length; i++)
            {
                Pad pad = pads[i];
                Vector3 position = pad.transform.localPosition;
                position.x = Mathf.Clamp(position.x + (i % 2 == 0 ? -directionSign : directionSign) * Time.deltaTime * moveSpeed, minX, maxX);
                pad.transform.localPosition = position;
            }
        }
    }
}