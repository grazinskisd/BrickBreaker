using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrickBreaker
{
    public class GameController : MonoBehaviour
    {
        public Ball ball;
        public float startForce;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ball.RigidBody2D.AddForce(Vector2.up * startForce);
            }
        }
    }
}