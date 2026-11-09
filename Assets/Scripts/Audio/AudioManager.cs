using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using Game.Core;

namespace Game.Audio
{
    public class AudioManager : MonoBehaviour, IVolumeChanger
    {
        [SerializeField] private AudioMixer _audioMixer;

        private IVolumeChanger _volumeChanger;

        public void Initialize(IVolumeChanger changer)
        {
            _volumeChanger = changer;
        }

        public void ChangeVolume(List<string> groups, float volume)
        {
            foreach (string group in groups)
            {
                float finalVolume = Mathf.Clamp(Mathf.Log10(volume) * 30, -80, 0);
                _audioMixer.SetFloat(group, finalVolume);
            }

            _volumeChanger.ChangeVolume(groups, volume);
        }
    }
}
