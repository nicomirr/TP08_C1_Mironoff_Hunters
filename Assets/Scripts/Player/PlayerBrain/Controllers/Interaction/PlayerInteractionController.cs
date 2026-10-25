using UnityEngine;
using Game.Core;
using UnityEngine.InputSystem;

public class PlayerInteractionController : MonoBehaviour
{
    private IInteractable _currentInteractable;

    private bool _isInteracting;
    public bool IsInteracting => _isInteracting;

    private void Update()
    {
        if (_currentInteractable == null) return;

        if(_currentInteractable.CanInteract && _isInteracting)
            _isInteracting = false;
    }

    public void Interact(InputAction interactAction)
    {        
        if (_currentInteractable == null) return;

        //esconder icono de interaccion posible       
                
        _isInteracting = true;
        _currentInteractable.Interact(interactAction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_currentInteractable != null) return;
        
        if (collision.TryGetComponent(out IInteractable interactable))
        {           
            if (!interactable.CanInteract) return;

            //mostrar signo de interactable
            _currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            if(_currentInteractable == interactable)            
                _currentInteractable = null;            
        }
    }
}
