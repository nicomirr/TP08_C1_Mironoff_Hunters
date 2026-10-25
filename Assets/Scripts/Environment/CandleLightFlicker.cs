using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Game.Environment
{ 
    public class CandleLightFlicker : MonoBehaviour
    {
        [SerializeField] private Light2D _light;

        [SerializeField] private float _minIntensity = 0.7f;
        [SerializeField] private float _maxIntensity = 1.2f;
        [SerializeField] private float _flickerSpeed = 4f;

        private float _noiseOffset;

        private void Awake()
        {
            _noiseOffset = Random.Range(0f, 1000f);
        }

        private void Update()
        {
            float noise = Mathf.PerlinNoise(
                Time.time * _flickerSpeed + _noiseOffset, 0f);

            _light.intensity = Mathf.Lerp(
                _minIntensity, _maxIntensity, noise);
        }
    }
}
