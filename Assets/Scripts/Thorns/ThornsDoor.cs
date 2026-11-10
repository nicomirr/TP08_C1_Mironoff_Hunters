using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Core;
using Game.Checkpoints;
using Game.GameManagement;

namespace Game.Thorns
{
    public class ThornsDoor : MonoBehaviour
    {
        [SerializeField] private List<ThornMaskController> _thornControllers = new();

        private AudioSource _audioSource;

        private bool _isOpen;
        public bool IsOpen => _isOpen;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>(); 
        }

        private void OnEnable()
        {
            Checkpoint.OnCheckpointUsed += OpenDoor;
        }

        private void Start()
        {
            foreach (ThornMaskController controller in _thornControllers)
                controller.Initialize(0.03f);

            _isOpen = true;
        }

        private void Update()
        {
            if (_isOpen) return;

            if(GameManager.BossKilled)
            {
                StartCoroutine(OpenDoorRoutine());
                _isOpen = true;
            }
        }

        private void OnDisable()
        {
            Checkpoint.OnCheckpointUsed -= OpenDoor;
        }

        public void OpenDoor(int _, bool __, PlayerType ___)
        {
            StartCoroutine(OpenDoorRoutine());
        }

        public void CloseDoor()
        {
            if (GameManager.BossKilled) return;

            StartCoroutine(CloseDoorRoutine()); 
        }

        private IEnumerator OpenDoorRoutine()
        {
            for (int i = _thornControllers.Count - 1; i >= 0; i--)
            {
                StartCoroutine(_thornControllers[i].HideRoutine());
                yield return new WaitForSeconds(0.03f);
            }

            _isOpen = true;
        }

        private IEnumerator CloseDoorRoutine()
        {
            _audioSource.Play();

            foreach (ThornMaskController thorn in _thornControllers)
            {
                StartCoroutine(thorn.RevealRoutine());
                yield return new WaitForSeconds(0.03f);
            }

            _isOpen = false;
        }

       
    }

}
