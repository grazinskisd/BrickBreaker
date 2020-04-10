using UnityEngine;

namespace BrickBreaker
{
    public class PeaceSet : MonoBehaviour
    {
        [HideInInspector]
        public Peace[] peaces;

        private void Awake()
        {
            peaces = new Peace[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                peaces[i] = transform.GetChild(i).GetComponent<Peace>();
            }
        }
    }
}