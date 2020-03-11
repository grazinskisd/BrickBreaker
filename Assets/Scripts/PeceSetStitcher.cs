using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrickBreaker
{
    public class PeceSetStitcher : MonoBehaviour
    {
        public GameObject peaceSetProto;
        [Range(4, 40)]
        public int splits;

        private int _oldSplits;

        private List<GameObject> _sets;

        private void Start()
        {
            _sets = new List<GameObject>();
            StitchSets();
        }

        private void StitchSets()
        {
            _oldSplits = splits;

            float angleBetweenSplits = 360f / splits;
            Vector3 result = Quaternion.AngleAxis((angleBetweenSplits / 2f), Vector3.forward) * Vector3.up;

            for (int i = 0; i < splits; i++)
            {
                var peaceSet = Instantiate(peaceSetProto, Vector3.zero, Quaternion.identity);
                peaceSet.transform.SetParent(transform);
                peaceSet.transform.localRotation = Quaternion.Euler(0, i % 2 == 0 ? 180 : 0, angleBetweenSplits * i + (angleBetweenSplits / 2f));
                _sets.Add(peaceSet);
            }
        }

        private void DeleteSets()
        {
            for (int i = 0; i < _sets.Count; i++)
            {
                Destroy(_sets[i]);
            }
            _sets.Clear();
        }

        private void Update()
        {
            if(_oldSplits != splits)
            {
                DeleteSets();
                StitchSets();
            }
        }
    }
}