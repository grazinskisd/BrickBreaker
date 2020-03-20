using UnityEngine;

namespace BrickBreaker
{
    public delegate void BallTriggerEnterEventHandler(Ball sender, Collider2D other);
    public delegate void BallCollisionEnterHandler(Ball sender, Collision2D collision);

    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball: MonoBehaviour
    {
        public event BallTriggerEnterEventHandler OnTriggerEnter;
        public event BallCollisionEnterHandler OnCollisionEnter;

        private Rigidbody2D _rigidBody;

        public Vector2 velocity;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _rigidBody.isKinematic = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEnter?.Invoke(this, collision);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnTriggerEnter?.Invoke(this, collision);
        }

        private void FixedUpdate()
        {
            transform.Translate(velocity * Time.deltaTime);
        }
    }
}