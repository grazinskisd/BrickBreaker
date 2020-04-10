using System.Collections.Generic;
using UnityEngine;

namespace BrickBreaker
{
    public class PadsController : MonoBehaviour
    {
        [Header("General settings")]
        public float moveSpeed;
        public BasePadPath padPath;

        private List<PadContainer> _padContainer;

        private void Awake()
        {
            _padContainer = new List<PadContainer>(transform.childCount);

            Vector3[] path = padPath.GetPathPoints();

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeInHierarchy)
                {
                    PadContainer container = transform.GetChild(i).GetComponent<PadContainer>();
                    container.PathLine.positionCount = path.Length;
                    container.PathLine.SetPositions(path);
                    _padContainer.Add(container);
                }
            }
        }

        private void Update()
        {
            UpdatePadPositionByMouse();
        }

        private void UpdatePadPositionByMouse()
        {
            float pathValue = padPath.MapMousePositionToPathPoint(Input.mousePosition);
            for (int i = 0; i < _padContainer.Count; i++)
            {
                padPath.UpdateToPathPoint(_padContainer[i], pathValue);
            }
        }
    }
}