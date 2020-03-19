using UnityEngine;

namespace BrickBreaker
{
    public delegate void BallTriggerEnterEventHandler(Collider2D other);
    public delegate void BallCollisionEnterHandler(Collision2D collision);

    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball: MonoBehaviour
    {
        public event BallTriggerEnterEventHandler OnTriggerEnter;
        public event BallCollisionEnterHandler OnCollisionEnter;

        private Rigidbody2D _rigidBody;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEnter?.Invoke(collision);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnTriggerEnter?.Invoke(collision);
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