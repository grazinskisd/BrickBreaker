using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrickBreaker
{
    public class Spin : MonoBehaviour
    {
        public float spinSpeed;

        void Update()
        {
            transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime, Space.Self);
        }
    }
}