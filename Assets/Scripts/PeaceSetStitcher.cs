﻿using System.Collections.Generic;
using UnityEngine;

namespace BrickBreaker
{
    public class PeaceSetStitcher : MonoBehaviour
    {
        public StitcherSettings settings;

        private int _oldSplits;

        private List<PeaceSet> _sets;

        public List<PeaceSet> PeaceSets
        {
            get
            {
                return _sets;
            }
        }

        private void Awake()
        {
            _sets = new List<PeaceSet>();
            StitchSets();
        }

        private void StitchSets()
        {
            _oldSplits = settings.splits;

            float angleBetweenSplits = 360f / settings.splits;
            Vector3 result = Quaternion.AngleAxis((angleBetweenSplits / 2f), Vector3.forward) * Vector3.up;

            for (int i = 0; i < settings.splits; i++)
            {
                var peaceSet = Instantiate(settings.peaceSetProto, Vector3.zero, Quaternion.identity);
                peaceSet.transform.SetParent(transform);
                peaceSet.transform.localRotation = Quaternion.Euler(0, 0, angleBetweenSplits * i);
                _sets.Add(peaceSet);
            }
        }

        private void DeleteSets()
        {
            for (int i = 0; i < _sets.Count; i++)
            {
                Destroy(_sets[i].gameObject);
            }
            _sets.Clear();
        }

        private void Update()
        {
            if(_oldSplits != settings.splits)
            {
                DeleteSets();
                StitchSets();
            }
        }
    }
}