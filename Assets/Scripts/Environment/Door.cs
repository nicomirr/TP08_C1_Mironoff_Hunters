using UnityEngine;
using Game.Core;
using UnityEngine.InputSystem;
using System;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _moveToTransform;
    [SerializeField] private float _newLeftCameraLimit;

    public static event Action<PlayerType, Transform, float> OnDoorUsed;

    private bool _canInteract;
    public bool CanInteract => _canInteract;
    public bool HasInteractorInRange { get; set; }


    private void Start()
    {
        _canInteract = true;
    }

    public void Interact(InputAction interactAction, Transform interactor, PlayerType playerType)
    {           
        interactor.position = _moveToTransform.position;
        OnDoorUsed?.Invoke(playerType, _moveToTransform,_newLeftCameraLimit);
    }    
}
