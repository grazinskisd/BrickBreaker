using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrickBreaker
{
    public delegate void PeaceCollisionHandler(Peace sender, Collision2D collision);

    public class Peace : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;

        public event PeaceCollisionHandler OnCollisionEnter;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEnter?.Invoke(this, collision);
        }
    }
}