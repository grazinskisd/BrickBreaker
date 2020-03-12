using UnityEngine;
using UnityEngine.SceneManagement;

namespace BrickBreaker
{
    public class GameController : MonoBehaviour
    {
        public Ball ball;
        public float startForce;
        public LayerMask killBoxLayerMask;

        private bool _isBallReleased;

        private void Start()
        {
            ball.OnTriggerEnter += CheckTrigger;
        }

        private void CheckTrigger(Collider2D other)
        {
            if (IsLayerInMask(other.gameObject.layer, killBoxLayerMask))
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
                ball.RigidBody2D.AddForce(Vector2.up * startForce);
                ball.transform.SetParent(null);
                _isBallReleased = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}