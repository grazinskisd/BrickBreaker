using UnityEngine;

namespace BrickBreaker
{
    public class PadContainer : MonoBehaviour
    {
        [SerializeField]
        private Pad _pad;

        [SerializeField]
        private LineRenderer _pathLine;

        [SerializeField]
        private Transform _padParent;

        public Transform PadParent { get => _padParent; }
        public LineRenderer PathLine { get => _pathLine; }
        public Pad Pad { get => _pad; }
    }
}