using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BrickBreaker
{
    public class GameController : MonoBehaviour
    {
        public Ball[] balls;
        public float startForce;
        public LayerMask killBoxLayerMask;
        public float maxBounceAngle;

        private bool _isBallReleased;
        private int _ballCount;

        private void Start()
        {
            for (int i = 0; i < balls.Length; i++)
            {
                SetupBall(balls[i]);
            }
            _ballCount = balls.Length;
        }

        private void SetupBall(Ball ball)
        {
            ball.OnTriggerEnter += CheckTrigger;
            ball.OnCollisionEnter += CheckCollision;
        }

        private void CheckCollision(Ball sender, Collision2D collision)
        {
            ContactPoint2D contact = collision.GetContact(0);

            if (contact.collider.CompareTag("Pad"))
            {
                Vector2 velocity = sender.RigidBody2D.velocity;
                BoxCollider2D collider = contact.collider.GetComponent<BoxCollider2D>();
                Vector2 collisionNormal = contact.normal;
                Vector2 boundsCenter = collider.bounds.center;
                Vector2 point = contact.point;
                Vector2 extents = collider.size * collider.transform.localScale * 0.5f;
                Vector2 center = new Vector2(boundsCenter.x, boundsCenter.y) + (collisionNormal * extents.y);
                float angleDir = AngleDir(Vector3.forward, point, center);
                float angle = angleDir * (Vector2.Distance(center, point) / extents.x) * maxBounceAngle;
                Vector2 newVelocityVector = Quaternion.AngleAxis(angle, Vector3.forward) * collisionNormal;
                sender.RigidBody2D.velocity = newVelocityVector * velocity.magnitude;
            }
        }

        //returns -1 when to the left, 1 to the right, and 0 for forward/backward
        public float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
        {
            Vector3 perp = Vector3.Cross(fwd, targetDir);
            float dir = Vector3.Dot(perp, up);

            if (dir > 0.0f)
            {
                return 1.0f;
            }
            else if (dir < 0.0f)
            {
                return -1.0f;
            }
            else
            {
                return 0.0f;
            }
        }

        private static void CreateSphere(Vector2 point)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.localScale = Vector3.one * 0.1f;
            go.transform.position = point;
        }

        private void CheckTrigger(Ball sender, Collider2D other)
        {
            if (IsLayerInMask(other.gameObject.layer, killBoxLayerMask))
            {
                Destroy(sender.gameObject);
                _ballCount--;
            }

            if(_ballCount == 0)
            {
                SceneManager.LoadScene(0);
            }
        }

        private bool IsLayerInMask(int layer, LayerMask mask)
        {
            return (mask == (mask | (1 << layer)));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_isBallReleased)
            {
                for (int i = 0; i < balls.Length; i++)
                {
                    LaunchBall(balls[i]);
                }
                _isBallReleased = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        private void LaunchBall(Ball ball)
        {
            ball.RigidBody2D.AddForce(ball.transform.up * startForce);
            ball.transform.SetParent(null);
        }
    }
}