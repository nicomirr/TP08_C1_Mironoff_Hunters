using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Game.Data;
using Game.Core;


namespace Game.SceneTransitions
{
    public class ZoneChanger : MonoBehaviour, IInteractable
    {
        [SerializeField] private SceneTransitionManager _sceneTransitionManager;

        [SerializeField] private List<SceneToLoadDataSo> _scenesToLoad;

        private bool _canInteract;
        public bool CanInteract => _canInteract;

        public bool HasInteractorInRange { get; set; }

        private void Start()
        {
            _canInteract = true;
        }

        public void Interact(InputAction interactAction, Transform interactor, PlayerType playerType)
        {
            _canInteract = false;

            _sceneTransitionManager.ChangeScene(_scenesToLoad[0]);
        }
    }
}

