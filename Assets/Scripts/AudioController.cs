using System;
using UnityEngine;

namespace BrickBreaker
{
    public class AudioController : MonoBehaviour
    {
        public AudioClip padBounce;
        public AudioClip ballDestroy;
        public AudioClip partDestroy;

        public AudioSource audioSource;

        public GameController gameController;
        public PeacesController peacesController;

        private void Start()
        {
            gameController.OnBallDestroy += PlayDestroySoundEffect;
            gameController.OnPadBounce += PlayBallBounceEffect;
            peacesController.OnPeaceDestroyed += PlayPeaceDestroyedEffect;
        }

        private void PlayPeaceDestroyedEffect()
        {
            audioSource.PlayOneShot(partDestroy);
        }

        private void PlayBallBounceEffect()
        {
            audioSource.PlayOneShot(padBounce);
        }

        private void PlayDestroySoundEffect()
        {
            audioSource.PlayOneShot(ballDestroy);
        }
    }
}