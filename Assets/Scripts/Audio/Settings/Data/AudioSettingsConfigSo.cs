using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "AudioSettingsConfigSo", menuName = "Scriptable Objects/AudioSettingsConfigSo")]
    public class AudioSettingsConfigSo : ScriptableObject
    {
        [SerializeField] private AudioGroupsSo _group;
        public AudioGroupsSo Group => _group;

        [Range(0f, 1f)][SerializeField] private float _defaultVolume;
        public float DefaultVolume => _defaultVolume;
    }

}

