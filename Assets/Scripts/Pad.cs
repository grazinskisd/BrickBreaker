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

        private int _previousDirection;

        private void Awake()
        {
            var main = _leftThrust.main;
            main.startColor = _color;
            main = _rightThrust.main;
            main.startColor = _color;
            _renderer.color = _color;
            _line.startColor = _color;
            _line.endColor = _color;
        }

        //private void Start()
        //{
        //    BoxCollider2D collider = GetComponent<BoxCollider2D>();

        //    Vector2 collisionNormal = transform.up;
        //    Bounds otherBounds = collider.bounds;
        //    Vector2 extents = collider.size * transform.localScale * 0.5f;
        //    Vector2 center = new Vector2(otherBounds.center.x, otherBounds.center.y) + (collisionNormal * extents.y);
        //    Debug.Log(extents);
        //    CreateSphere(center);
        //    Vector2 point = center;

        //    float part = extents.x / 10;

        //    for (int i = -10; i <= 10; i++)
        //    {
        //        var right = transform.right * (part * i);
        //        point = center + new Vector2(right.x, right.y);

        //        float angle = AngleDir(Vector3.forward, point, center) * (Vector2.Distance(center, point) / extents.x) * 60;
        //        Vector2 newVelocityVector = Quaternion.AngleAxis(angle, Vector3.forward) * collisionNormal;
        //        Debug.DrawLine(point, point + newVelocityVector, Color.red, 10);
        //    }
        //}

        //public float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
        //{
        //    Vector3 perp = Vector3.Cross(fwd, targetDir);
        //    float dir = Vector3.Dot(perp, up);

        //    if (dir > 0.0f)
        //    {
        //        return 1.0f;
        //    }
        //    else if (dir < 0.0f)
        //    {
        //        return -1.0f;
        //    }
        //    else
        //    {
        //        return 0.0f;
        //    }
        //}

        //private static void CreateSphere(Vector2 point)
        //{
        //    var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //    go.transform.localScale = Vector3.one * 0.1f;
        //    go.transform.position = point;
        //}

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("PAD COLLISION WITH " + collision.gameObject.name);
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

        public void SetThrustEmiting(int direction)
        {
            if (_previousDirection == direction) return;

            _previousDirection = direction;
            if(direction == 0)
            {
                SetThrustEmiting(_leftThrust, false);
                SetThrustEmiting(_rightThrust, false);
            }
            else if (direction == 1)
            {
                SetThrustEmiting(_leftThrust, false);
                SetThrustEmiting(_rightThrust, true);
            }
            else if (direction == -1)
            {
                SetThrustEmiting(_leftThrust, true);
                SetThrustEmiting(_rightThrust, false);
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
}