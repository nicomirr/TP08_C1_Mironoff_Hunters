using UnityEngine;

namespace Game.Audio
{
    public class AudioVisibilityController : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
                
        private void OnBecameVisible()
        {
            _audioSource.volume = 1f;
        }

        private void OnBecameInvisible()
        {
            _audioSource.volume = 0f;
        }
    }

}
