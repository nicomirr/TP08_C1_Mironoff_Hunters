using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Data;
using Game.Checkpoints;

namespace Game.Thorns
{
    public class ThornsDoor : MonoBehaviour
    {
        [SerializeField] private List<ThornMaskController> _thornControllers = new();

        private bool _isOpen;
        public bool IsOpen => _isOpen;

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

        private void OnDisable()
        {
            Checkpoint.OnCheckpointUsed -= OpenDoor;
        }

        public void OpenDoor(int _)
        {
            StartCoroutine(OpenDoorRoutine());
        }

        public void CloseDoor()
        {
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
            foreach (ThornMaskController thorn in _thornControllers)
            {
                StartCoroutine(thorn.RevealRoutine());
                yield return new WaitForSeconds(0.03f);
            }

            _isOpen = false;
        }

       
    }

}
