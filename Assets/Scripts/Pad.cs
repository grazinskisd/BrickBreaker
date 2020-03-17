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
        }

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