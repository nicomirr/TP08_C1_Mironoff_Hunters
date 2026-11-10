using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Game.Core;

namespace Game.Environment
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform _moveToTransform;
        [SerializeField] private float _newLeftCameraLimit;

        public static event Action<PlayerType, Transform, float> OnDoorUsed;

        private AudioSource _audioSource;

        private bool _canInteract;
        public bool CanInteract => _canInteract;
        public bool HasInteractorInRange { get; set; }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _canInteract = true;
        }

        public void Interact(InputAction interactAction, Transform interactor, PlayerType playerType)
        {
            _audioSource.Play();

            interactor.position = _moveToTransform.position;
            OnDoorUsed?.Invoke(playerType, _moveToTransform, _newLeftCameraLimit);
        }
    }

}
