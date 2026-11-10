using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Data;

namespace Game.Thorns
{
    public class ThornTrapController : MonoBehaviour
    {
        [SerializeField] private ThornTrapControllerDataSo _data;

        [SerializeField] private List<ThornMaskController> _thornControllers = new();

        [SerializeField] private float _initialDelayTime;

        private AudioSource _audioSource;

        private bool _isAlive;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _isAlive = true;

            foreach (ThornMaskController controller in _thornControllers)
                controller.Initialize(_data.ThornSpawnTime);

            StartCoroutine(TrapRoutine());
        }

        private IEnumerator TrapRoutine()
        {

            yield return new WaitForSeconds(_initialDelayTime);            

            while (_isAlive)
            {                
                _audioSource.Play();

                foreach (ThornMaskController thorn in _thornControllers)
                {
                    StartCoroutine(thorn.RevealRoutine());
                    yield return new WaitForSeconds(_data.ThornSpawnTime);
                }

                yield return new WaitForSeconds(_data.ActiveTime);

                _audioSource.Play();

                for (int i = _thornControllers.Count - 1; i >= 0; i--)
                {
                    StartCoroutine(_thornControllers[i].HideRoutine());
                    yield return new WaitForSeconds(_data.ThornSpawnTime);
                }

                yield return new WaitForSeconds(_data.InactiveTime);

            }

        }

    }
}


