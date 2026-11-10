using UnityEngine;
using System.Collections;
using Game.Data;

namespace Game.Environment
{
    public class RainController : MonoBehaviour
    {
        [SerializeField] private RainControllerDataSo _data;

        [SerializeField] private ParticleSystem _rainParticles;
        [SerializeField] private Animator _thunderAnimator;
        [SerializeField] private AudioSource _thunderAudio;

        private bool _isRaining;

        private void Start()
        {
            _isRaining = true;

            _rainParticles.Play();
            StartCoroutine(RainRoutine());
        }

        private IEnumerator RainRoutine()
        {
            while (_isRaining)
            {
                int randomTime = Random.Range(_data.MinThunderTime, _data.MaxThunderTime);

                yield return new WaitForSeconds(randomTime);

                _thunderAnimator.SetTrigger(_data.ThunderAnimationTriggerHash);

                float randTime = Random.Range(1, 3);

                yield return new WaitForSeconds(randTime);

                _thunderAudio.Play();
            }

            _rainParticles.Stop();
        }
    }

}
