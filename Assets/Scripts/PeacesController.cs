using UnityEngine;
using UnityEngine.UI;

namespace BrickBreaker
{
    public class PeacesController : MonoBehaviour
    {
        public Text scoreText;
        public PeaceSetStitcher stitcher;

        private int _score;

        private void Start()
        {
            for (int i = 0; i < stitcher.PeaceSets.Count; i++)
            {
                for (int j = 0; j < stitcher.PeaceSets[i].peaces.Length; j++)
                {
                    Peace peace = stitcher.PeaceSets[i].peaces[j];
                    peace.OnCollisionEnter += ProcessPeaceCollision;
                }
            }
        }

        private void ProcessPeaceCollision(Peace sender, Collision2D collision)
        {
            Destroy(sender.gameObject);
            _score++;
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            scoreText.text = _score.ToString();
        }
    }
}