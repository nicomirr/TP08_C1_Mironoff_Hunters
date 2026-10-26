using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Core
{
    public interface IInteractable
    {
        public bool CanInteract { get; }
        public bool HasInteractorInRange { get; set; }
        public void Interact(InputAction interactAction, Transform interactor, PlayerType playerType);
    }

}
