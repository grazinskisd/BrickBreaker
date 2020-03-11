using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrickBreaker
{
    public class PatternGenerator : MonoBehaviour
    {
        public GameObject[] protos;
        [Range(4, 40)]
        public int splits;
        [Range(1, 10)]
        public float radius;
        [Range(4, 40)]
        public int peaces;

        private int _oldSplits;
        private int _oldPieces;
        private float _oldRadius;

        private List<GameObject> _pieces;

        void Start()
        {
            _pieces = new List<GameObject>();
            DrawSplits();
        }

        private void DrawSplits()
        {
            _oldSplits = splits;
            _oldPieces = peaces;
            _oldRadius = radius;
            float angleBetweenSplits = 360f / splits;

            Vector3 result = Quaternion.AngleAxis((angleBetweenSplits / 2f), Vector3.forward) * Vector3.up;
            List<GameObject> linePeaces = new List<GameObject>();
            for (int j = 0; j < peaces; j++)
            {
                var peace = Instantiate(protos[Random.Range(0, protos.Length)], Vector3.zero, Quaternion.identity);
                SetupPeace(angleBetweenSplits, result, peace, 0, j);
                linePeaces.Add(peace);
            }

            for (int i = 1; i < splits; i++)
            {
                result = Quaternion.AngleAxis(angleBetweenSplits * i + (angleBetweenSplits/2f), Vector3.forward) * Vector3.up;
                for (int j = 0; j < peaces; j++)
                {
                    SetupPeace(angleBetweenSplits, result, Instantiate(linePeaces[j]), i, j);
                }
            }
        }

        private void SetupPeace(float angleBetweenSplits, Vector3 result, GameObject peace, int splitId, int peaceInSplit)
        {
            peace.transform.SetParent(transform);
            peace.transform.localPosition = (result * radius / peaces) * peaceInSplit;
            peace.transform.localRotation = Quaternion.Euler(0, 0, angleBetweenSplits * splitId + (angleBetweenSplits / 2f));
            //peace.transform.localScale = Vector3.one * (peaceInSplit * (radius / peaces) * (angleBetweenSplits*0.1f));
            peace.transform.localScale = new Vector3(1, 5*(radius / peaces), 1);
            _pieces.Add(peace);
        }

        private void DeletePeaces()
        {
            for (int i = 0; i < _pieces.Count; i++)
            {
                Destroy(_pieces[i]);
            }
        }

        void Update()
        {
            if(_oldSplits != splits || _oldPieces != peaces || _oldRadius != radius)
            {
                DeletePeaces();
                DrawSplits();
            }
        }
    }
}