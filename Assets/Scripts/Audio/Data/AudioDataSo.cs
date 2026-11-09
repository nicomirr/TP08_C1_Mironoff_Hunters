using UnityEngine;
using Game.Core;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "AudioDataSo", menuName = "Scriptable Objects/AudioDataSo")]
    public class AudioDataSo : ScriptableObject
    {
        [SerializeField] private AudioClip _audioClip;  
        public AudioClip AudioClip => _audioClip;

        [SerializeField] private AudioCategory _audioCategory;
        public AudioCategory AudioCategory => _audioCategory;
    }
}

