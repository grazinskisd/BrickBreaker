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
                    Debug.Log("Add pad");
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
                MovePads(-1);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                MovePads(1);
            }
        }

        private void StopPads()
        {
            for (int i = 0; i < _pads.Count; i++)
            {
                _pads[i].SetThrustEmiting(0);
            }
        }

        private void MovePads(int directionSign)
        {
            for (int i = 0; i < _pads.Count; i++)
            {
                Pad pad = _pads[i];
                Vector3 position = pad.transform.localPosition;
                int direction = (i % 2 == 0 ? -directionSign : directionSign);
                position.x = Mathf.Clamp(position.x + direction * Time.deltaTime * moveSpeed, minX, maxX);
                pad.transform.localPosition = position;
                pad.SetThrustEmiting(direction);
            }
        }
    }
}