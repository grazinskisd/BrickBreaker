using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BrickBreaker
{
    public delegate void PeaceEventHandler();

    public class PeacesController : MonoBehaviour
    {
        public Text scoreText;
        public Map map;
        public ParticleSystem destroyFXProto;

        public event PeaceEventHandler OnPeaceDestroyed;

        private int _score;
        private int _peaceCount;

        public int PeaceCount
        {
            get
            {
                return _peaceCount;
            }
        }

        private void Awake()
        {
            for (int i = 0; i < map.sets.Count; i++)
            {
                for (int j = 0; j < map.sets[i].peaces.Length; j++)
                {
                    Peace peace = map.sets[i].peaces[j];
                    peace.OnCollisionEnter += ProcessPeaceCollision;
                    _peaceCount++;
                }
            }
        }

        private void ProcessPeaceCollision(Peace sender, Collision2D collision)
        {
            var destroyEffect = Instantiate(destroyFXProto);
            var shape = destroyEffect.shape;
            shape.spriteRenderer = sender.spriteRenderer;
            var main = destroyEffect.main;
            main.startColor = sender.spriteRenderer.color;

            sender.gameObject.SetActive(false);
            StartCoroutine(DestroyDelayed(sender, main.startLifetime.constant));

            _score++;
            UpdateScoreText();
            OnPeaceDestroyed?.Invoke();
        }

        private IEnumerator DestroyDelayed(Peace peace, float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(peace.gameObject);
        }

        private void UpdateScoreText()
        {
            scoreText.text = _score.ToString();
        }
    }
}