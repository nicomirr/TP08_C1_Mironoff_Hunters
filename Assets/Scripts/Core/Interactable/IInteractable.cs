using UnityEngine.InputSystem;

namespace Game.Core
{
    public interface IInteractable
    {
        public bool CanInteract { get; }
        public void Interact(InputAction interactAction);
    }

}
