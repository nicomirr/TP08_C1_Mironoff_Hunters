using UnityEngine;
using System.Collections.Generic;
using Game.Data;
using Game.Core;

namespace Game.Settings
{
    public class GameAudioSettings : MonoBehaviour, IVolumeProvider, IVolumeChanger
    {
        [SerializeField] private AudioSettingsConfigSo _data;

        private Dictionary<string, float> _volumes;
        public Dictionary<string, float> Volumes => _volumes;

        private void Awake()
        {           
            _volumes = new Dictionary<string, float>();

            foreach(string group in _data.Group.AudioGroups)
            {
                _volumes.Add(group, _data.DefaultVolume);
            }
        }

        public void ChangeVolume(List<string> groups, float volume)
        {
            foreach (string group in groups)
            {
                _volumes[group] = volume;
            }
        }
    }
}


