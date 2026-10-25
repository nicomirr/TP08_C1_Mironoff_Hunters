using UnityEngine;
using System.Collections;

public class RainController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _rainParticles;
    [SerializeField] private Animator _thunderAnimator;

    private void Start()
    {
        _rainParticles.Play();
        StartCoroutine(RainRoutine());
    }

    private IEnumerator RainRoutine()
    {
        while (true)
        {
            int randomTime = Random.Range(10, 11);
            
            yield return new WaitForSeconds(randomTime);

            _thunderAnimator.SetTrigger("Thunder");
        }
    }
}
