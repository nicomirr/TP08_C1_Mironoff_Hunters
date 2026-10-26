using UnityEngine;
using UnityEngine.InputSystem;
using Game.Core;
using System;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private PlayerType _playerType;

    private IInteractable _currentInteractable;

    private bool _isInteracting;
    public bool IsInteracting => _isInteracting;

    public event Action OnInteractableInRange;
    public event Action OnInteractableOutOfRange;

    private void Update()
    {
        if (_currentInteractable == null) return;

        if(_currentInteractable.CanInteract && _isInteracting)
        {
            _isInteracting = false;
            OnInteractableInRange?.Invoke();
        }
    }

    public void Interact(InputAction interactAction)
    {        
        if (_currentInteractable == null) return;
        if (!_currentInteractable.CanInteract) return;

        OnInteractableOutOfRange?.Invoke();      
                
        _isInteracting = true;
        _currentInteractable.Interact(interactAction, this.transform, _playerType);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_currentInteractable != null) return;
        
        if (collision.TryGetComponent(out IInteractable interactable))
        {            
            OnInteractableInRange?.Invoke();

            interactable.HasInteractorInRange = true;
            _currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            if(_currentInteractable == interactable)
            {
                _currentInteractable = null;
                _isInteracting = false;
                interactable.HasInteractorInRange = false;
                OnInteractableOutOfRange?.Invoke();
            }
        }
    }
}
