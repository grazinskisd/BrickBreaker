﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrickBreaker
{
    public class Peace : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("PEACE COLLISION WITH " + collision.gameObject.name);
        }
    }
}