using UnityEngine;

namespace BrickBreaker
{
    public class PadsController : MonoBehaviour
    {
        public float minX;
        public float maxX;
        public float moveSpeed;

        private Pad[] _pads;

        private void Awake()
        {
            _pads = new Pad[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                _pads[i] = transform.GetChild(i).GetChild(0).GetComponent<Pad>();
            }
        }

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
            for (int i = 0; i < _pads.Length; i++)
            {
                Pad pad = _pads[i];
                Vector3 position = pad.transform.localPosition;
                position.x = Mathf.Clamp(position.x + (i % 2 == 0 ? -directionSign : directionSign) * Time.deltaTime * moveSpeed, minX, maxX);
                pad.transform.localPosition = position;
            }
        }
    }
}