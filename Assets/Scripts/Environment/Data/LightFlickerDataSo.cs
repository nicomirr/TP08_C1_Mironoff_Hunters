using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "LightFlickerDataSo", menuName = "Scriptable Objects/LightFlickerDataSo")]
    public class LightFlickerDataSo : ScriptableObject
    {
        [SerializeField] private float _minIntensity;
        public float MinIntensity => _minIntensity;

        [SerializeField] private float _maxIntensity;
        public float MaxIntensity => _maxIntensity;

        [SerializeField] private float _flickerSpeed;
        public float FlickerSpeed => _flickerSpeed;

        [SerializeField] private float _maxNoiseOffset;
        public float MaxNoiseOffset => _maxNoiseOffset;
    }

}

