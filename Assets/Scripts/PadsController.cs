using System.Collections.Generic;
using UnityEngine;

namespace BrickBreaker
{
    public class PadsController : MonoBehaviour
    {
        public float minX;
        public float maxX;
        public float moveSpeed;

        private List<Pad> _pads;

        private void Awake()
        {
            _pads = new List<Pad>(transform.childCount);
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeInHierarchy)
                {
                    _pads.Add(transform.GetChild(i).GetChild(0).GetComponent<Pad>());
                }
            }
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                StopPads();
            }

            if (Input.GetKey(KeyCode.A))
            {
                MovePads(-moveSpeed);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                MovePads(moveSpeed);
            }

            UpdatePositionByMouse();
        }

        private void UpdatePositionByMouse()
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            mouseScreenPosition.z = Camera.main.transform.position.z;
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            float xPosition = Mathf.Clamp(mousePositionInWorld.x, minX, maxX);

            for (int i = 0; i < _pads.Count; i++)
            {
                Pad pad = _pads[i];
                Vector3 position = pad.transform.localPosition;
                position.x = xPosition;
                pad.transform.localPosition = position;
            }
        }

        private void StopPads()
        {
            for (int i = 0; i < _pads.Count; i++)
            {
                _pads[i].SetThrustDiretion(Direction.None);
            }
        }

        private void MovePads(float direction)
        {
            for (int i = 0; i < _pads.Count; i++)
            {
                Pad pad = _pads[i];
                Vector3 position = pad.transform.localPosition;
                direction = (i % 2 == 0 ? -direction : direction);
                position.x = Mathf.Clamp(position.x + direction * Time.deltaTime, minX, maxX);
                pad.transform.localPosition = position;
                pad.SetThrustDiretion(direction > 0 ? Direction.Right : Direction.Left);
            }
        }
    }
}