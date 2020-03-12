using UnityEngine;

namespace BrickBreaker
{
    public delegate void BallTriggerEnterEventHandler(Collider2D other);

    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball: MonoBehaviour
    {
        public event BallTriggerEnterEventHandler OnTriggerEnter;
        private Rigidbody2D _rigidBody;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
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