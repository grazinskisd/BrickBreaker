using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrickBreaker
{
    public class Pad : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _leftThrust;

        [SerializeField]
        private ParticleSystem _rightThrust;

        [SerializeField]
        private LineRenderer _line;

        [SerializeField]
        private SpriteRenderer _renderer;

        [SerializeField]
        public Color _color;

        [Header("Debug")]
        [SerializeField]
        private bool _shouldDrawBounceLines;

        private Direction _previousDirection;
        private BoxCollider2D _collider;

        public BoxCollider2D Collider
        {
            get
            {
                return _collider;
            }
        }

        private void Awake()
        {
            var main = _leftThrust.main;
            main.startColor = _color;
            main = _rightThrust.main;
            main.startColor = _color;
            _renderer.color = _color;
            _line.startColor = _color;
            _line.endColor = _color;
            _collider = GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            if (_shouldDrawBounceLines)
            {
                DrawBounceLines();
            }
        }

        private void DrawBounceLines()
        {
            BoxCollider2D collider = GetComponent<BoxCollider2D>();

            Vector2 collisionNormal = transform.up;
            Bounds otherBounds = collider.bounds;
            Vector2 extents = collider.size * transform.localScale * 0.5f;
            Vector2 center = new Vector2(otherBounds.center.x, otherBounds.center.y) + (collisionNormal * extents.y);
            Debug.Log(extents);
            CreateSphere(center);
            Vector2 point = center;

            float part = extents.x / 10;

            for (int i = -10; i <= 10; i++)
            {
                var right = transform.right * (part * i);
                point = center + new Vector2(right.x, right.y);

                float angle = AngleDir(Vector3.forward, point, center) * (Vector2.Distance(center, point) / extents.x) * 30;
                Vector2 newVelocityVector = Quaternion.AngleAxis(angle, Vector3.forward) * collisionNormal;
                Debug.DrawLine(point, point + newVelocityVector, Color.red, 10);
            }
        }

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

        public bool IsLeftEmiting
        {
            set
            {
                SetThrustEmiting(_leftThrust, value);
            }
        }

        public bool IsRightEmiting
        {
            set
            {
                SetThrustEmiting(_rightThrust, value);
            }
        }

        public void SetThrustDiretion(Direction direction)
        {
            if (_previousDirection == direction) return;

            _previousDirection = direction;
            switch (direction)
            {
                case Direction.None:
                    SetThrustEmiting(_leftThrust, false);
                    SetThrustEmiting(_rightThrust, false);
                    break;
                case Direction.Left:
                    SetThrustEmiting(_leftThrust, false);
                    SetThrustEmiting(_rightThrust, true);
                    break;
                case Direction.Right:
                    SetThrustEmiting(_leftThrust, true);
                    SetThrustEmiting(_rightThrust, false);
                    break;
                default:
                    break;
            }
        }

        private void SetThrustEmiting(ParticleSystem thrust, bool isEmiting)
        {
            if (isEmiting)
            {
                thrust.Play();
            }
            else
            {
                thrust.Stop();
            }
        }
    }

    public enum Direction
    {
        None, Left, Right
    }
}