﻿using UnityEngine;
using UnityEngine.UI;

namespace BrickBreaker
{
    public delegate void PeaceEventHandler();

    public class PeacesController : MonoBehaviour
    {
        public Text scoreText;
        public Map map;

        public event PeaceEventHandler OnPeaceDestroyed;

        private int _score;

        private void Start()
        {
            for (int i = 0; i < map.sets.Count; i++)
            {
                for (int j = 0; j < map.sets[i].peaces.Length; j++)
                {
                    Peace peace = map.sets[i].peaces[j];
                    peace.OnCollisionEnter += ProcessPeaceCollision;
                }
            }
        }

        private void ProcessPeaceCollision(Peace sender, Collision2D collision)
        {
            Destroy(sender.gameObject);
            _score++;
            UpdateScoreText();
            OnPeaceDestroyed?.Invoke();
        }

        private void UpdateScoreText()
        {
            scoreText.text = _score.ToString();
        }
    }
}