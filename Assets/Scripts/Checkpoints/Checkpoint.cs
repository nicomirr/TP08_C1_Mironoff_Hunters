using UnityEngine;
using UnityEngine.InputSystem;
using Game.Core;
using System;

namespace Game.Checkpoints
{
    public class Checkpoint : MonoBehaviour, IInteractable
    {
        private bool _canInteract;
        public bool CanInteract => _canInteract;

        public bool HasInteractorInRange { get; set ; }

        public static event Action OnCheckpointUsed;

        private void Start()
        {
            _canInteract = true;
        }

        public void Interact(InputAction interactAction, Transform interactor, PlayerType playerType)
        {
            OnCheckpointUsed?.Invoke();
        }
    }

}
