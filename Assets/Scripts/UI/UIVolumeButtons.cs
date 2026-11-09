using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Game.Core;
using Game.Data;

namespace Game.UI
{
    public class UIVolumeButtons : MonoBehaviour
    {
        [SerializeField] private GameObject _volumeManagerChangerGameObject;

        [SerializeField] private Image _volumeFill;

        [SerializeField] private Button _decreaseVolumeButton;
        [SerializeField] private Button _increaseVolumeButton;

        [SerializeField] private VolumeButtonsTypeSo _data;

        private AudioSource _audioSource;

        private IVolumeProvider _volumeProvider;
        private IVolumeChanger _volumeChanger;

        private void Awake()
        {           
            _decreaseVolumeButton.onClick.AddListener(OnVolumeDecrease);
            _increaseVolumeButton.onClick.AddListener(OnVolumeIncrease);

            _audioSource = GetComponent<AudioSource>(); 

            _volumeChanger = _volumeManagerChangerGameObject.GetComponent<IVolumeChanger>();
        }

        private void OnDestroy()
        {
            _decreaseVolumeButton.onClick.RemoveAllListeners();
            _increaseVolumeButton.onClick.RemoveAllListeners();
        }

        public void Initialize(IVolumeProvider provider)
        {
            _volumeProvider = provider;
            InitializeSliderValue(_volumeProvider.Volumes);
        }

        private void Start()
        {
            NotifyVolumeChange();
        }

        private void InitializeSliderValue(Dictionary<string, float> volumes)
        {           
            foreach(KeyValuePair<string, float> pair in volumes)
            {
                if(_data.VolumeTypes.Contains(pair.Key))
                {
                    _volumeFill.fillAmount = pair.Value;
                    break;
                }
            }            
        }

        private void OnVolumeIncrease()
        {
            _volumeFill.fillAmount = Mathf.Min(_volumeFill.fillAmount + 0.1f, 1f);
            NotifyVolumeChange();

            _audioSource.Play();
        }

        private void OnVolumeDecrease()
        {
            _volumeFill.fillAmount = Mathf.Max(_volumeFill.fillAmount - 0.1f, 0f);
            NotifyVolumeChange();

            _audioSource.Play();
        }

        private void NotifyVolumeChange()
        {
            _volumeChanger.ChangeVolume(_data.VolumeTypes, _volumeFill.fillAmount);
        }
    }
}

