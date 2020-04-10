using UnityEngine;

namespace BrickBreaker
{
    [CreateAssetMenu(menuName = "BrickBreaker/ArchPadPath")]
    public class ArchPadPath : BasePadPath
    {
        public int pathSegments;
        public float maxPadAngle;
        public float pathYOffset;
        public float maxAngle;

        public override Vector3[] GetPathPoints()
        {
            Vector3[] arcPoints = new Vector3[pathSegments];
            float angle = -maxPadAngle;
            float arcLength = 2 * maxPadAngle;
            for (int i = 0; i < pathSegments; i++)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * pathYOffset;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * pathYOffset;

                arcPoints[i] = new Vector3(x, y);

                angle += (arcLength / pathSegments);
            }
            return arcPoints;
        }

        public override float MapMousePositionToPathPoint(Vector3 mousePosition)
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            float half = Screen.width / 2;
            float centeredMousePosition = half - mouseScreenPosition.x;
            float rotation = (centeredMousePosition / half) * maxAngle;
            float clampedRotation = Mathf.Clamp(rotation, -maxPadAngle, maxPadAngle);
            return clampedRotation;
        }

        public override void UpdateToPathPoint(PadContainer container, float pathPoint)
        {
            container.PadParent.transform.localRotation = Quaternion.Euler(0, 0, pathPoint);
        }
    }
}
