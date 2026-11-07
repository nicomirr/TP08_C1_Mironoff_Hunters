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

        private bool _isAlive;

        private void Start()
        {
            _isAlive = true;

            StartCoroutine(RevealRoutine());
        }

        private IEnumerator RevealRoutine()
        {

            yield return new WaitForSeconds(_initialDelayTime);            

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


