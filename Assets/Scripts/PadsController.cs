using System;
using System.Collections.Generic;
using UnityEngine;

namespace BrickBreaker
{
    public class PadsController : MonoBehaviour
    {
        [Header("General settings")]
        public float moveSpeed;
        public bool isLinearMovement;
        public float pathYOffset;

        [Header("Linear movement")]
        public float maxX;

        [Header("Circular movement")]
        public float maxAngle;
        public float maxPadAngle;
        public int pathSegments;

        private List<PadContainer> _padContainer;

        private void Awake()
        {
            _padContainer = new List<PadContainer>(transform.childCount);
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeInHierarchy)
                {
                    PadContainer container = transform.GetChild(i).GetComponent<PadContainer>();
                    _padContainer.Add(container);

                    if (isLinearMovement)
                    {
                        DrawLinearPath(container.PathLine);
                    }
                    else
                    {
                        DrawCurvedPath(container.PathLine);
                    }
                }
            }
        }

        private void DrawLinearPath(LineRenderer path)
        {
            Vector3 startPoint = new Vector3(-maxX, pathYOffset, 0);
            Vector3 endPoint = new Vector3(maxX, pathYOffset, 0);
            path.positionCount = 2;
            path.SetPositions(new Vector3[] { startPoint, endPoint });
        }

        private void DrawCurvedPath(LineRenderer pathLine)
        {
            Vector3[] arcPoints = new Vector3[pathSegments];
            float angle = -maxPadAngle;
            float arcLength = 2*maxPadAngle;
            for (int i = 0; i < pathSegments; i++)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * pathYOffset;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * pathYOffset;

                arcPoints[i] = new Vector3(x, y);

                angle += (arcLength / pathSegments);
            }
            pathLine.positionCount = pathSegments;
            pathLine.SetPositions(arcPoints);
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

            if (isLinearMovement)
            {
                UpdatePositionLinearByMouse();
            }
            else
            {
                UpdateRotationByMouse();
            }
        }

        private void UpdatePositionLinearByMouse()
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            mouseScreenPosition.z = Camera.main.transform.position.z;
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            float xPosition = Mathf.Clamp(mousePositionInWorld.x, -maxX, maxX);

            for (int i = 0; i < _padContainer.Count; i++)
            {
                Pad pad = _padContainer[i].Pad;
                Vector3 position = pad.transform.localPosition;
                position.x = xPosition;
                pad.transform.localPosition = position;
            }
        }

        private void UpdateRotationByMouse()
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            float half = Screen.width / 2;
            float centeredMousePosition = half - mouseScreenPosition.x;
            float rotation = (centeredMousePosition / half) * maxAngle;
            float clampedRotation = Mathf.Clamp(rotation, -maxPadAngle, maxPadAngle);

            for (int i = 0; i < _padContainer.Count; i++)
            {
                Transform padParent = _padContainer[i].PadParent;
                padParent.transform.localRotation = Quaternion.Euler(0, 0, clampedRotation);
            }
        }

        private void StopPads()
        {
            for (int i = 0; i < _padContainer.Count; i++)
            {
                _padContainer[i].Pad.SetThrustDiretion(Direction.None);
            }
        }

        private void MovePads(float direction)
        {
            for (int i = 0; i < _padContainer.Count; i++)
            {
                Pad pad = _padContainer[i].Pad;
                Vector3 position = pad.transform.localPosition;
                direction = (i % 2 == 0 ? -direction : direction);
                position.x = Mathf.Clamp(position.x + direction * Time.deltaTime, -maxX, maxX);
                pad.transform.localPosition = position;
                pad.SetThrustDiretion(direction > 0 ? Direction.Right : Direction.Left);
            }
        }
    }
}