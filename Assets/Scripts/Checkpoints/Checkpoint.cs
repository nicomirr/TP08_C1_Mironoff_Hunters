using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using System;
using Game.Core;
using Game.Data;
using Game.Animations;

namespace Game.Checkpoints
{
    public class Checkpoint : MonoBehaviour, IInteractable
    {
        [SerializeField] private int _checkpointNumber;
        public int CheckpointNumber => _checkpointNumber;

        [SerializeField] private CheckpointDataSo _data;        

        private AnimationsController _animationsController;

        private Light2D _light;

        private bool _canInteract;
        public bool CanInteract => _canInteract;

        public bool HasInteractorInRange { get; set ; }

        public static event Action<int> OnCheckpointUsed;

        private void Awake()
        {
            _animationsController = new AnimationsController(_data.AnimationData, GetComponentInChildren<Animator>(), AnimationStateType.Disabled);
            _light = GetComponentInChildren<Light2D>();
        }

        private void Start()
        {
            _canInteract = true;
        }

        public void Interact(InputAction interactAction, Transform interactor, PlayerType playerType)
        {
            OnCheckpointUsed?.Invoke(_checkpointNumber);

            _animationsController.ChangeAnimationState(AnimationStateType.Enabled);
            _light.enabled = true;

        }

        public void Reset()
        {
            _animationsController.ChangeAnimationState(AnimationStateType.Disabled);
            _light.enabled = false;
        }

    }

}
