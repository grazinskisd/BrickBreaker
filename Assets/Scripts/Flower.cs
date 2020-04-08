using UnityEngine;

namespace BrickBreaker
{
    public class Flower : MonoBehaviour
    {
        [SerializeField]
        private Peace[] _peaces;

        public Peace[] Peaces
        {
            get
            {
                return _peaces;
            }
        }
    }
}