using UnityEngine;

namespace BrickBreaker
{
    [CreateAssetMenu(menuName = "BrickBreaker/LinearPadPath")]
    public class LinearPadPath : BasePadPath
    {
        public float maxX;
        public float pathYOffset;

        public override Vector3[] GetPathPoints()
        {
            Vector3 startPoint = new Vector3(-maxX, pathYOffset, 0);
            Vector3 endPoint = new Vector3(maxX, pathYOffset, 0);
            return new Vector3[] { startPoint, endPoint };
        }

        public override float MapMousePositionToPathPoint(Vector3 mousePosition)
        {
            mousePosition.z = Camera.main.transform.position.z;
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
            float xPosition = Mathf.Clamp(mousePositionInWorld.x, -maxX, maxX);
            return xPosition;
        }

        public override void UpdateToPathPoint(PadContainer container, float pathPoint)
        {
            Pad pad = container.Pad;
            Vector3 position = pad.transform.localPosition;
            position.x = pathPoint;
            pad.transform.localPosition = position;
        }
    }
}
