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
                // Suggestion: el valor no depende de group, calculalo fuera del foreach. La conversión estándar a dB es Log10(volume) * 20; el 30 y el -80 conviene ponerlos como const o en AudioSettingsConfigSo.
                float finalVolume = Mathf.Clamp(Mathf.Log10(volume) * 30, -80, 0);
                _audioMixer.SetFloat(group, finalVolume);
            }

            _volumeChanger.ChangeVolume(groups, volume);
        }
    }
}
