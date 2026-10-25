using UnityEngine;
using UnityEngine.Rendering.Universal;
using Game.Data;

namespace Game.Environment
{ 
    public class CandleLightFlicker : MonoBehaviour
    {
        [SerializeField] private Light2D _light;

        [SerializeField] private LightFlickerDataSo _data;

        private float _noiseOffset;

        private void Awake()
        {
            _noiseOffset = Random.Range(0f, _data.MaxNoiseOffset);
        }

        private void Update()
        {
            float noise = Mathf.PerlinNoise(Time.time * _data.FlickerSpeed + _noiseOffset, 0f);

            _light.intensity = Mathf.Lerp(_data.MinIntensity, _data.MaxIntensity, noise);
        }
    }
}
