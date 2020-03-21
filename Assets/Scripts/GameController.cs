using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BrickBreaker
{
    public delegate void BallEventHandler();

    public class GameController : MonoBehaviour
    {
        [Header("Ball setup")]
        public Ball ballPrototype;
        public Pad[] startPads;
        public Vector2 ballStartPosition;
        public float startVelocity;
        public LayerMask killBoxLayerMask;
        public float maxBounceAngle;

        [Header("Life setup")]
        public int startLives;
        public Text lifesText;

        public event BallEventHandler OnPadBounce;
        public event BallEventHandler OnBallDestroy;

        private Ball[] _balls;
        private bool _isBallReleased;
        private int _ballCount;

        private int _currentLives;

        private void Start()
        {
            _currentLives = startLives;
            UpdateLivesText();
            SetupStartingBalls();
        }

        private void SetupStartingBalls()
        {
            _balls = new Ball[startPads.Length];
            for (int i = 0; i < startPads.Length; i++)
            {
                Ball ball = Instantiate(ballPrototype, Vector3.zero, Quaternion.identity);
                SetupBall(startPads[i], ball);
                _balls[i] = ball;
            }
            _ballCount = _balls.Length;
            _isBallReleased = false;
        }

        private void UpdateLivesText()
        {
            lifesText.text = _currentLives.ToString();
        }

        private void SetupBall(Pad pad, Ball ball)
        {
            ball.transform.SetParent(pad.transform);
            ball.transform.localPosition = ballStartPosition;

            ball.OnTriggerEnter += CheckTrigger;
            ball.OnCollisionEnter += CheckCollision;
        }

        private void CheckCollision(Ball sender, Collision2D collision)
        {
            ContactPoint2D contact = collision.GetContact(0);

            if (contact.collider.CompareTag("Pad"))
            {
                BounceBallOffPad(sender, contact);
                OnPadBounce?.Invoke();
            }
            else
            {
                NormalBounce(sender, contact);
            }
        }

        private void NormalBounce(Ball sender, ContactPoint2D contact)
        {
            var velocity = sender.velocity;
            var speed = velocity.magnitude;
            var direction = Vector3.Reflect(velocity.normalized, contact.normal);
            sender.velocity = direction * speed;
        }

        private void BounceBallOffPad(Ball sender, ContactPoint2D contact)
        {
            BoxCollider2D collider = contact.collider.GetComponent<Pad>().Collider;
            Vector2 collisionNormal = contact.normal;
            Vector2 boundsCenter = collider.bounds.center;
            Vector2 point = contact.point;
            Vector2 extents = collider.size * collider.transform.localScale * 0.5f;
            Vector2 center = new Vector2(boundsCenter.x, boundsCenter.y) + (collisionNormal * extents.y);
            float angleDir = AngleDir(Vector3.forward, point, center);
            float angle = angleDir * (Vector2.Distance(center, point) / extents.x) * maxBounceAngle;
            Vector2 newVelocityVector = Quaternion.AngleAxis(angle, Vector3.forward) * collisionNormal;
            sender.velocity = newVelocityVector * sender.velocity.magnitude;
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
                DestroyBall(sender);
            }

            if (_ballCount == 0)
            {
                _currentLives--;
                UpdateLivesText();

                if (_currentLives > 0)
                {
                    SetupStartingBalls();
                }
                else
                {
                    StartCoroutine(RestartSceneDelayed());
                }
            }
        }

        private void DestroyBall(Ball sender)
        {
            sender.OnTriggerEnter -= CheckTrigger;
            sender.OnCollisionEnter -= CheckCollision;
            sender.StartDestructionSequence();
            StartCoroutine(DestroyDelayed(sender));
            _ballCount--;
            OnBallDestroy?.Invoke();
        }

        private IEnumerator RestartSceneDelayed()
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(0);
        }

        private IEnumerator DestroyDelayed(Ball ball)
        {
            yield return new WaitForSeconds(1);
            Destroy(ball.gameObject);
        }

        private bool IsLayerInMask(int layer, LayerMask mask)
        {
            return (mask == (mask | (1 << layer)));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_isBallReleased)
            {
                for (int i = 0; i < _balls.Length; i++)
                {
                    LaunchBall(_balls[i]);
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
            ball.velocity = (ball.transform.up * startVelocity);
            ball.transform.SetParent(null);
        }
    }
}