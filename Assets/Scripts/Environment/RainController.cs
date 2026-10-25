using UnityEngine;
using System.Collections;
using Game.Data;

public class RainController : MonoBehaviour
{
    [SerializeField] private RainControllerDataSo _data;

    [SerializeField] private ParticleSystem _rainParticles;
    [SerializeField] private Animator _thunderAnimator;

    private bool _isRaining;

    private void Start()
    {
        _rainParticles.Play();
        StartCoroutine(RainRoutine());

        _isRaining = true;
    }

    private IEnumerator RainRoutine()
    {
        while (_isRaining)
        {
            int randomTime = Random.Range(_data.MinThunderTime, _data.MaxThunderTime);
            
            yield return new WaitForSeconds(randomTime);

            _thunderAnimator.SetTrigger(_data.ThunderAnimationTriggerHash);
        }

        _rainParticles.Stop();
    }
}
