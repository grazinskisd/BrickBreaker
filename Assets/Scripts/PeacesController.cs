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

        public Peace debugPece;

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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                ProcessPeaceCollision(debugPece, null);
            }
        }

        private void ProcessPeaceCollision(Peace sender, Collision2D collision)
        {
            Flower flower = sender.GetComponent<Flower>();

            if (flower != null)
            {
                for (int i = 0; i < flower.Peaces.Length; i++)
                {
                    var peace = flower.Peaces[i];
                    if (peace != null && peace.isActiveAndEnabled)
                    {
                        DestroyPeace(flower.Peaces[i]);
                    }
                }
            }
            DestroyPeace(sender);
        }

        private void DestroyPeace(Peace peace)
        {
            peace.OnCollisionEnter -= ProcessPeaceCollision;

            var main = CreateDestroyEffect(peace);
            peace.gameObject.SetActive(false);
            StartCoroutine(DestroyDelayed(peace, main.startLifetime.constant));

            _score++;
            UpdateScoreText();
            OnPeaceDestroyed?.Invoke();
        }

        private ParticleSystem.MainModule CreateDestroyEffect(Peace peace)
        {
            var destroyEffect = Instantiate(destroyFXProto);
            var shape = destroyEffect.shape;
            shape.spriteRenderer = peace.spriteRenderer;
            var main = destroyEffect.main;
            main.startColor = peace.spriteRenderer.color;
            return main;
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