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

        private bool _isAlive;

        private void Start()
        {
            _isAlive = true;

            StartCoroutine(RevealRoutine());
        }

        private IEnumerator RevealRoutine()
        {
            float initialDelayTime = Random.Range(_data.MinInitialDelayTime, _data.MaxInitialDelayTime);

            yield return new WaitForSeconds(initialDelayTime);            

            while (_isAlive)
            {                
                foreach (ThornMaskController thorn in _thornControllers)
                {
                    StartCoroutine(thorn.RevealRoutine());
                    yield return new WaitForSeconds(_data.ThornSpawnTime);
                }

                yield return new WaitForSeconds(_data.ActiveTime);

                for(int i = _thornControllers.Count - 1; i >= 0; i--)
                {
                    StartCoroutine(_thornControllers[i].HideRoutine());
                    yield return new WaitForSeconds(_data.ThornSpawnTime);
                }

                yield return new WaitForSeconds(_data.InactiveTime);

            }

        }

    }
}


