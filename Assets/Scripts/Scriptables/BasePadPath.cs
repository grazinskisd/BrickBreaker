using UnityEngine;

namespace BrickBreaker
{
    public abstract class BasePadPath : ScriptableObject
    {
        public abstract Vector3[] GetPathPoints();
        public abstract float MapMousePositionToPathPoint(Vector3 mousePosition);
        public abstract void UpdateToPathPoint(PadContainer container, float pathPoint);
    }
}