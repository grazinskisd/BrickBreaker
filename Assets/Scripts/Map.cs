using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrickBreaker
{
    public class Map : MonoBehaviour
    {
        [HideInInspector]
        public List<PeaceSet> sets;

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                sets.Add(transform.GetChild(i).GetComponent<PeaceSet>());
            }
        }
    }
}