using UnityEngine;

namespace BrickBreaker
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball: MonoBehaviour
    {
        private Rigidbody2D _rigidBody;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        public Rigidbody2D RigidBody2D
        {
            get
            {
                return _rigidBody;
            }
        }
    }
}