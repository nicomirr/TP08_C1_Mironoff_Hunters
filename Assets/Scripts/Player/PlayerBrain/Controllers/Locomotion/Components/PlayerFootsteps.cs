using UnityEngine;

namespace Game.Player
{
    public class PlayerFootsteps
    {
        private readonly AudioSource _audioSource;

        public PlayerFootsteps(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }

        public void PlayFootsteps()
        {
            if (_audioSource.isPlaying) return;

            _audioSource.Play();
        }
    }

}
